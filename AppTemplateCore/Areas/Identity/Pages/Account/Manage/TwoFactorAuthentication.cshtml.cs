using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.Identity.Pages.Account.Manage
{
    public class TwoFactorAuthenticationModel : PageModel
    {
        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<TwoFactorAuthenticationModel> _logger;

        public TwoFactorAuthenticationModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<TwoFactorAuthenticationModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // UI Conditional Rendering Flag
        public bool HasAuthenticator { get; set; }

        // Info to show in Page
        public int RecoveryCodesLeft { get; set; }

        // Only one Property needed when page Post Back
        [BindProperty]
        public bool Is2faEnabled { get; set; }

        // UI conditional Rendering Flag
        public bool IsMachineRemembered { get; set; }

        // Temp Data
        [TempData]
        public string StatusMessage { get; set; }


        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = $"Unable to load user with ID '{_userManager.GetUserId(User)}'.";
                return NotFound();
            }

            // Returns the authenticator app key for the user.
            // Flag whether user has Auth Key or Not so that UI could be customized
            // & relevant Options shown
            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;

            // Returns a flag indicating whether the specified user has two factor authentication
            // enabled or not, UI Customization, RelevantOptions shown
            Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);

            // Returns a flag indicating if the current client browser has been remembered by
            // two factor authentication for the user attempting to login
            // Another Flag for UI Options cutomization
            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);

            // Returns how many recovery code are still valid for a user.
            // Another Variable for UI Options to be shown
            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

            return Page();
        }


        // User's browser will forget about the Users 2FA Code
        // Next time he will be prompted for 2FA Code.
        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = $"Unable to load user with ID '{_userManager.GetUserId(User)}'.";
                return NotFound();
            }

            //     Clears the "Remember this browser flag" from the current browser
            await _signInManager.ForgetTwoFactorClientAsync();
            StatusMessage = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.";

            // Same page redirect, it is like calling OnGet()
            return RedirectToPage();
        }
    }
}