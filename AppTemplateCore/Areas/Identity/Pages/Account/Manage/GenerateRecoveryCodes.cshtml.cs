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
    public class GenerateRecoveryCodesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<GenerateRecoveryCodesModel> _logger;

        public GenerateRecoveryCodesModel(
            UserManager<ApplicationUser> userManager,
            ILogger<GenerateRecoveryCodesModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [TempData]
        public string[] RecoveryCodes { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        // Show the Screen so that User can GenerateRecoverCodes
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = $"Unable to load user with ID '{_userManager.GetUserId(User)}'.";
                return NotFound();
            }

            // Returns a flag indicating whether the specified user has 2FA
            // enabled or not, based on this UI will be customized and 
            // relevant options will be provided on UI
            var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            if (!isTwoFactorEnabled)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{userId}' because they do not have 2FA enabled.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = $"Unable to load user with ID '{_userManager.GetUserId(User)}'.";
                return NotFound();
            }

            // Returns a flag indicating whether the specified user has 2FA
            // enabled or not, based on this UI will be customized and 
            // relevant options will be provided on UI
            var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{userId}' as they do not have 2FA enabled.");
            }

            // Generates recovery codes for the user, this invalidates any previous recovery
            // codes for the user.
            // The number of codes to generate.
            // The new recovery codes for the user. 
            // Note: there may be less than number returned,
            // as duplicates will be removed.
            // WHERE THESE RECOVERY CODES ARE STORED, IN DB ???
            // BECAUSE RECOVER CODES GIVE ACCESS WHEN AUTHENTICATOR APP IS NOT WORKING
            // MOBILE IS NOT WITH YOU, AND ARE USING APPLICATION ON DESKTOP COMPUTER
            // BUT STILL WE NEED ACCESS TO THE WEB TO ACCESS THE WWW WEB APPLICATION.
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            RecoveryCodes = recoveryCodes.ToArray();

            // Log important User Actions or DB State Changing Operations
            _logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", userId);

            // Set the status message to show on the Page
            StatusMessage = "You have generated new recovery codes.";

            // Page ShowRecoveryCodes does not exist, create it yourself
            return RedirectToPage("./ShowRecoveryCodes");
        }
    }
}