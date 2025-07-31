// Areas/Identity/Pages/Account/Manage/TwoFactorAuthentication.cshtml.cs
using System.Threading.Tasks;
using Azaliq.Data.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Azaliq.WebApp.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class TwoFactorAuthenticationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<TwoFactorAuthenticationModel> _logger;

        public TwoFactorAuthenticationModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<TwoFactorAuthenticationModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        // Existing flags
        public bool HasAuthenticator { get; set; }
        public bool Is2faEnabled { get; set; }
        public bool IsEmail2faEnabled { get; set; }
        public bool IsMachineRemembered { get; set; }
        public int RecoveryCodesLeft { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound($"Unable to load user.");

            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
            Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            IsEmail2faEnabled = (await _userManager.GetValidTwoFactorProvidersAsync(user))
                                       .Contains(TokenOptions.DefaultEmailProvider);
            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostForgetBrowserAsync()
        {
            await _signInManager.ForgetTwoFactorClientAsync();
            StatusMessage = "This browser was forgotten—next login will require 2FA code.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEnableEmail2faAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound($"Unable to load user.");

            // Turn on 2FA if not already
            if (!await _userManager.GetTwoFactorEnabledAsync(user))
                await _userManager.SetTwoFactorEnabledAsync(user, true);

            StatusMessage = "Email-based two-factor authentication enabled.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDisableEmail2faAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound($"Unable to load user.");

            // To disable email-2FA alone we'd need to remove email provider from valid list
            // but Identity doesn’t expose that directly, so here we simply turn off 2FA entirely
            await _userManager.SetTwoFactorEnabledAsync(user, false);

            StatusMessage = "Email-based two-factor authentication disabled.";
            return RedirectToPage();
        }
    }
}