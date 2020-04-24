using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.Identity.Pages.Account
{
    // Whole Controller is for anonymout user
    [AllowAnonymous]
    public class ConfirmEmailModel : AccountPageModel
    {

        public ConfirmEmailModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ConfirmEmailModel> logger)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Logger = logger;
        }

        // This method is called by clicking the Confirm Email send to 
        // User Mailbox
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                TempData["ErrorMessage"] = $"Unable to load user with ID '{UserManager.GetUserId(User)}'.";
                return NotFound();
            }

            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["ErrorMessage"] = $"Unable to load user with ID '{UserManager.GetUserId(User)}'.";
                return NotFound();
            }

            // Validates that an email confirmation token matches the specified user.
            // The user to validate the token against.
            // The email confirmation token to validate.
            var result = await UserManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");

            }

            return Page();
        }
    }
}
