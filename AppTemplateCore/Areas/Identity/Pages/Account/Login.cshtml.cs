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
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace AppTemplateCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : AccountPageModel
    {
        private readonly IEmailSender EmailSender;

        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly ILogger<LoginModel> _logger;

        //public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
        //{
        //    _signInManager = signInManager;
        //    _logger = logger;
        //}

        public LoginModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            IEmailSender emailSender)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Logger = logger;
            EmailSender = emailSender;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        // Non-Post Back info needed on page
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [TempData]
        public string EmailConfirmationToken { get; set; }

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
            if (!string.IsNullOrEmpty(StatusMessage))
            {
                ModelState.AddModelError(string.Empty, StatusMessage);
            }

            // If returnURL is null, set it to root.
            // After success full login redirect to that return url
            // View Model hold that Prperty.
            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Gets a collection of AuthenticationSchemes
            // for the known external login providers.
            ExternalLogins = (await SignInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }




        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            ExternalLogins = (await SignInManager.GetExternalAuthenticationSchemesAsync()).ToList();

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

                ////////////////////////////EMAIL CONFIRMATION ////////////////////////////////
                var user = await UserManager.FindByEmailAsync(Input.Email);

                // Email confirmation check added here. Now also add the logic to 
                // the database seed logic that add default user to the DB
                if (user != null && !user.EmailConfirmed &&
                                    (await UserManager.CheckPasswordAsync(user, Input.Password)))
                {
                    // Generate Email Confirmation Token for the User.
                    // This code is send to the User' Email for confirmation
                    var emailConfirmationToken = await UserManager.GenerateEmailConfirmationTokenAsync(user);

                    // UserId & EmailConfirmationToken is included in the Email Confirmation Link
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = emailConfirmationToken },
                        protocol: Request.Scheme);

                    EmailConfirmationToken = callbackUrl;

                    // Send Email to the users email.
                    await EmailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    return RedirectToPage("./ConfirmEmailInfoAfterLogin");

                    //ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    //return Page();
                }
                ////////////////////////////EMAIL CONFIRMATION ////////////////////////////////

                var result = await SignInManager.PasswordSignInAsync(
                                                    Input.Email,
                                                    Input.Password,
                                                    Input.RememberMe,
                                                    lockoutOnFailure: true);

                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        Logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        // If URL is blank/null or URL not local URL, send user to homepage
                        return LocalRedirect(Url.Content("~/"));
                        //return RedirectToAction("index", "home");
                    }

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
                    Logger.LogWarning("User account locked out.");
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
