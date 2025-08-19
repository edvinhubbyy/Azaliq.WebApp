using Azaliq.Data.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Azaliq.WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public LoginWith2faModel(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public class InputModel
        {
            [Required]
            [Display(Name = "Security code")]
            public string TwoFactorCode { get; set; } = null!;
        }

        public async Task<IActionResult> OnGetAsync(bool rememberMe, string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? "/";
            RememberMe = rememberMe;

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return RedirectToPage("./Login");
            }
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
            var email = await _userManager.GetEmailAsync(user);

            await _emailSender.SendEmailAsync(
                email,
                "Your 2FA Security Code",
                $"Your security code is: <strong>{code}</strong>");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(bool rememberMe, string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? "/";
            RememberMe = rememberMe;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _signInManager.TwoFactorSignInAsync(
                TokenOptions.DefaultEmailProvider,
                Input.TwoFactorCode.Replace(" ", string.Empty),
                rememberMe,
                rememberClient: false);

            if (result.Succeeded)
            {
                return LocalRedirect(ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }

            ModelState.AddModelError(string.Empty, "Invalid authentication code.");
            return Page();
        }
    }
}