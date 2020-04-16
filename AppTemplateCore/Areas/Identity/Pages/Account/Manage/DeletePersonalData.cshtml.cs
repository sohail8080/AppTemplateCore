using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;

        public DeletePersonalDataModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeletePersonalDataModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        // Variable for Conditional Rendering of Page
        // Not related to UI Controls or Model posted back for Post()
        public bool RequirePassword { get; set; }

        // Show blank screen that either user want to delete his account with the site
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = $"Unable to load user with ID '{_userManager.GetUserId(User)}'.";
                return NotFound();
            }

            // Gets a flag indicating whether the specified user has a password.
            // It may possible that user do not have local user account
            // if user has local user accont then get password for deleting the user
            // Flag to render the Password Field on the UI
            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        // Post Operation that user want to delete his account with the site
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = $"Unable to load user with ID '{_userManager.GetUserId(User)}'.";
                return NotFound();
            }

            // Gets a flag indicating whether the specified user has a password.
            // It may possible that user do not have local user account
            // if user has local user accont then get password for deleting the user
            // Flag to render the Password Field on the UI
            // Also used when UI is re-rendered in case of error
            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                // Returns a flag indicating whether the given password is 
                // valid for the specified
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Password not correct.");
                    return Page();
                }
            }

            // Deletes the specified user from the backing store.
            var result = await _userManager.DeleteAsync(user);
            // Gets the user identifier for the specified user.
            var userId = await _userManager.GetUserIdAsync(user);

            if (!result.Succeeded)
            {
                // DB operation failed, and exception failed.
                throw new InvalidOperationException($"Unexpected error occurred deleteing user with ID '{userId}'.");
            }

            // Signs the current user out of the application.
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}