using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Azaliq.Data.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;

namespace Azaliq.WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
                          UserManager<ApplicationUser> userManager,
                          ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public string ReturnUrl { get; set; } = "/";

        public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();

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

            if (!ModelState.IsValid)
            {
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
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
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                return Page();
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "You need to confirm your email to log in.");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
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
                ModelState.AddModelError(string.Empty, "User account locked out.");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                return Page();
            }
        }
    }
}