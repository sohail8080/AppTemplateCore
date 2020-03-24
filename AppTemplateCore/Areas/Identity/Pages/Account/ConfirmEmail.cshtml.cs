using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppTemplateCore.Areas.Identity.Pages.Account
{
    // Whole Controller is for anonymout user
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // This method is called by clicking the Confirm Email send to 
        // User Mailbox
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            //     Validates that an email confirmation token matches the specified user.
            //     The user to validate the token against.
            //     The email confirmation token to validate.
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return Page();
        }
    }
}
