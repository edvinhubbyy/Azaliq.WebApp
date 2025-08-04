using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Security; // Your IReCaptchaService namespace
using Azaliq.Services.Core.Security.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Azaliq.WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private const string FailedAttemptsSessionKey = "FailedLoginAttempts";

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IReCaptchaService _reCaptchaService;
        private readonly IConfiguration _configuration;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<LoginModel> logger,
            IReCaptchaService reCaptchaService,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _reCaptchaService = reCaptchaService;
            _configuration = configuration;
        }

        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public string ReturnUrl { get; set; } = "/";

        public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();

        [Required(ErrorMessage = "Please confirm you are not a robot.")]
        [BindProperty(Name = "g-recaptcha-response")]
        public string RecaptchaToken { get; set; } = string.Empty;

        public string ReCaptchaSiteKey => _configuration["GoogleReCaptcha:SiteKey"];

        public class InputModel
        {
            [Required]
            [Display(Name = "Username or Email")]
            public string UsernameOrEmail { get; set; } = null!;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = null!;

            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? "/";
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            
            if (string.IsNullOrEmpty(RecaptchaToken) || !await _reCaptchaService.VerifyAsync(RecaptchaToken))
            {
                ModelState.AddModelError("RecaptchaToken", "Please confirm you are not a robot.");
                return Page();
            }
            

            // Find user by username or email
            var user = await _userManager.FindByNameAsync(Input.UsernameOrEmail);
            if (user == null && Input.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(Input.UsernameOrEmail);
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "You need to confirm your email to log in.");
                return Page();
            }

            // Lockout enabled: lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(
                user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                // Reset failed attempts on successful login
                HttpContext.Session.Remove(FailedAttemptsSessionKey);

                _logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }
            else if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { RememberMe = Input.RememberMe, ReturnUrl = returnUrl });
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                ModelState.AddModelError(string.Empty, "User account locked out. Please try again later.");
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

        private async Task IncrementFailedAttemptsAsync(int currentFailedAttempts)
        {
            currentFailedAttempts++;
            HttpContext.Session.SetInt32(FailedAttemptsSessionKey, currentFailedAttempts);

            // Optional: log the increment
            _logger.LogWarning($"Failed login attempt #{currentFailedAttempts}");
        }
    }
}