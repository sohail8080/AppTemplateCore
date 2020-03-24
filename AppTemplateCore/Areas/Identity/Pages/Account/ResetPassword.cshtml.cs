using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppTemplateCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // ViewModel for the View
        [BindProperty]
        public InputModel Input { get; set; }

        // Inline View Model definition
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        //I think we come on to this page by Email Link
        // This Screen is shown when the user Forget the password
        // Reset Password Link is send to the User's Email 
        // User Clicke the Email Link and Reset the Password.
        // I think, this Code Part is there inside the Link
        // Code is the Reset Token provided inside the Email LInke
        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = code
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                // Great Tip, it is there for fooling the Hacker who want to 
                // Know if an Email is registered on a site or not.
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            // Resets the user's password to the specified newPassword after validating the
            // given password reset token.
            // user: The user whose password should be reset.
            // token: The password reset token to verify.
            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                // When System State in DB changed, then log that info
                // ????????????????????????

                // Password reset successsfully
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            // populate errors on the page
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Show same page with error 
            return Page();
        }
    }
}
