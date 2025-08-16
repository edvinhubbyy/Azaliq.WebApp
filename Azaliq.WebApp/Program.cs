using Azaliq.Data;
using Azaliq.Data.Configurations;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core;
using Azaliq.Services.Core.Contracts;
using Azaliq.Services.Core.Security;
using Azaliq.Services.Core.Security.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Get port from environment or default
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

// Set URLs before building the app
if (builder.Environment.IsDevelopment())
{
    // Use HTTPS with localhost and default dev port
    builder.WebHost.UseUrls($"https://localhost:{port}");
}
else
{
    // Azure usually expects HTTP on the assigned port
    builder.WebHost.UseUrls($"http://*:{port}");
}

// 1) Configure EF Core and ApplicationDbContext
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 2) Configure Identity with email confirmation & 2FA token providers
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // Sign-in settings
        options.SignIn.RequireConfirmedEmail = true;
        options.SignIn.RequireConfirmedAccount = true;
        options.SignIn.RequireConfirmedPhoneNumber = false;

        // Password settings
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;

        // Token provider settings
        options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Add Google Authentication
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

        // Ensure Google sends email claim
        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

        options.Events.OnRemoteFailure = context =>
        {
            context.Response.Redirect("/Identity/Account/Login");
            context.HandleResponse(); // Prevent exception
            return Task.CompletedTask;
        };
    })
    .AddGitHub(options =>
    {
        options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];

        options.Scope.Add("user:email");
        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

        options.Events.OnRemoteFailure = context =>
        {
            context.Response.Redirect("/Identity/Account/Login");
            context.HandleResponse();
            return Task.CompletedTask;
        };
    });

// 3) Map the "Email" token provider for email confirmation & 2FA
builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Tokens.ProviderMap["Email"] =
        new TokenProviderDescriptor(typeof(EmailTokenProvider<ApplicationUser>));
    opts.Tokens.EmailConfirmationTokenProvider = "Email";
});

// 4) Register application services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IArchivedUserService, ArchivedUserService>();
builder.Services.AddScoped<IPdfService, PdfService>();

// Security
builder.Services.AddHttpClient();
builder.Services.AddScoped<IReCaptchaService, ReCaptchaService>();

// 5) Register email senders
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IEmailService, EmailService>();

// 6) Add MVC + Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 7) Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSession();

// Seed roles on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    RoleSeeder.AssignRoles(services);
}

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseAuthentication();
app.UseAuthorization();


// **AREA ROUTE FIRST**
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();