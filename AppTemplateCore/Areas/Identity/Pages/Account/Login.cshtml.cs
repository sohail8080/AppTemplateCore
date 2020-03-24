using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AppTemplateCore.Areas.AccessControl.Models;

namespace AppTemplateCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        // Non-Post Back info needed on page
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        // Non-Post Back info needed on page
        public string ReturnUrl { get; set; }

        // Non-Post Back info needed on page
        [TempData]
        public string ErrorMessage { get; set; }

        // Post Back info needed on page
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        // Show the Login Screen
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // If returnURL is null, set it to root.
            // After success full login redirect to that return url
            // View Model hold that Prperty.
            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Gets a collection of AuthenticationSchemes
            // for the known external login providers.
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, 
                // set lockoutOnFailure: true

                //  Attempts to sign in the specified userName and password combination 
                //  as an asynchronous operation.
                //  userName: The user name to sign in.
                //  password: The password to attempt to sign in with.
                //  isPersistent: Flag indicating whether the sign-in cookie should persist 
                //  after the browser is closed.
                //   lockoutOnFailure: Flag indicating if the user account should be locked if the sign in fails.

                // Success, Failed, LockedOut, NotAllowed, TwoFactorRequired
                // Succeeded, IsLockedOut, IsNotAllowed, RequiresTwoFactor

                // If 2fa is enabled, this method automatically send the Code to the User Mobile
                // OR WE NEED TO WRITE CODE HERE FOR EMAIL AND SMS FOR 2FA.?????


                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    // In case of 2FA, when user provides valid UserName & Password
                    // he is directed here. in case of wrong username and password
                    // reult is invalid login.
                    // WE ARE NOT SENDING ANY CALL/CODE TO THE AUTHENTICATOR APPLICATION.
                    // HOW THE APPLICATION WILL KNOW THAT USER IS GOING TO LOGIN AND I NEED 
                    // TO PROVIDE A CODE THE USER.

                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
