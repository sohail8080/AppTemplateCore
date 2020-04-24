using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ExternalLoginModel> _logger;

        private readonly IEmailSender EmailSender;

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            EmailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        // Info needed on UI Rendering
        // This info is lost when page is post back
        // If you need this back, after failed OnPost()
        // you need to refill it in the OnPost()
        public string LoginProvider { get; set; }

        // Info Needed in Razor Page
        public string ReturnUrl { get; set; }

        [TempData]
        public string EmailConfirmationToken { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        // One Property Class
        public class InputModel
        {

            [Required]
            [Display(Name = "First Name")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string LastName { get; set; }


            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }


        public IActionResult OnGetAsync()
        {
            // Like Calling another page OnGet()
            return RedirectToPage("./Login");
        }

        // This method is called by the Login.cshtml when the External Login buttons are pressed
        // On external login
        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });

            // Summary:
            //     Configures the redirect URL and user identifier for the specified external login
            //     provider.
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        // After giving credentials to the External Website, we are redirected to 
        // following Controller Actions. this happens every time user logins to the website 
        // by external provider
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            // if null then send to home page
            returnUrl = returnUrl ?? Url.Content("~/");

            // some error occurred on remote server
            if (remoteError != null)
            {
                // this ErrorMessage will be shown on login page as we re redirecting
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Gets the external login information for the current login attempt
            // no error, then get the external login info
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();

            // no external login info found
            if (externalLoginInfo == null)
            {
                // this ErrorMessage will be shown on login page as we re redirecting
                // We may redirect here to specific page that ext login fails
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            ////////////////////////////EMAIL CONFIRMATION ////////////////////////////////

            // get email from the external login info
            var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);

            if (email == null)
            {
                // Email claim not received
                ErrorMessage = $"Email claim not received from: {externalLoginInfo.LoginProvider}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            // if we found email then
            // get the user from local database
            // user exist in local db, then check email confirmaiton

            if (user != null && !user.EmailConfirmed)
            {

                // Generate Email Confirmation Token for the User.
                // This code is send to the User' Email for confirmation
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // UserId & EmailConfirmationToken is included in the Email Confirmation Link
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code = emailConfirmationToken },
                    protocol: Request.Scheme);

                EmailConfirmationToken = callbackUrl;

                // Send Email to the users email.
                await EmailSender.SendEmailAsync(email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToPage("./ConfirmEmailInfoAfterLogin");


                // if user exist in db and email is not confirmed
                // then remain on same page and show error message
                //ErrorMessage = "Email not confirmed yet.";
                //return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }


            ////////////////////////////EMAIL CONFIRMATION ////////////////////////////////

            // Sign in the user with this external login provider if the user already has a login.
            // Signin the user with the credential provided.
            // it also checks user exist in the aspnetusers & aspnetuserlogin tables
            // This creates cookie for the user. 
            // no password verification is done, as it is done already.
            var result = await _signInManager.ExternalLoginSignInAsync(
                                                externalLoginInfo.LoginProvider,
                                                externalLoginInfo.ProviderKey,
                                                isPersistent: false,
                                                bypassTwoFactor: true);

            // user signin in successfully, after ext login provider verify password
            if (result.Succeeded)
            {
                // it happens when user is already inserted in aspnetusers, aspnetuserlogins table
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", externalLoginInfo.Principal.Identity.Name, externalLoginInfo.LoginProvider);
                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                // it happens when user is already inserted in aspnetusers, aspnetuserlogins table
                // but account is locked for sometime.
                return RedirectToPage("./Lockout");
            }
            else
            {
                
                if(user != null)
                {
                    // Adds an external UserLoginInfo to the specified
                    // user in aspnetuserlogins table
                    var resultAddLogin = await _userManager.AddLoginAsync(user, externalLoginInfo);

                    if (!resultAddLogin.Succeeded)
                    {
                        // if we reached this far, some error occured
                        foreach (var error in resultAddLogin.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        LoginProvider = externalLoginInfo.LoginProvider;
                        ReturnUrl = returnUrl;
                        return Page();
                    }


                    // Signs in the specified user. create the cookie
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created an account using {Name} provider.", externalLoginInfo.LoginProvider);
                    return LocalRedirect(returnUrl);
                }
                
                
                // If the user does not have an account, then ask the user to create an account.
                // it happens when user is not inserted in aspnetusers, aspnetuserlogins table
                // show this page again with email
                ReturnUrl = returnUrl;

                // get login provider form External Login Info
                // needed as we going to render the smae page.
                // same page need that string property
                LoginProvider = externalLoginInfo.LoginProvider;

                // get Email Claim from the External Login info
                if (externalLoginInfo.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }

                // Show the same page with Email for the user registration
                return Page();
            }
        }


        // After Verifying by External Login, save User to Local DB in two tables 
        // If the user does not have an account, then ask the user to create an account.
        // it happens when user is not inserted in aspnetusers, aspnetuserlogins table.
        // show this page again with email
        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("~/");

            // Get the information about the user from the external login provider
            // Gets the external login information for the current login attempt
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();

            // External login info not found
            if (externalLoginInfo == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (!ModelState.IsValid)
            {
                LoginProvider = externalLoginInfo.LoginProvider;
                ReturnUrl = returnUrl;
                return Page();
            }

            var existingUser = await _userManager.FindByEmailAsync(Input.Email);

            //var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };

            var user = new ApplicationUser
            {

                UserName = Input.Email,
                Email = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName
            };


            IdentityResult result;

            if (existingUser == null)
            {
                // create user in the aspnetusers table
                result = await _userManager.CreateAsync(user);


                if (!result.Succeeded)
                {
                    // if we reached this far, some error occured
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    LoginProvider = externalLoginInfo.LoginProvider;
                    ReturnUrl = returnUrl;
                    return Page();
                }

                ////////////////////////////EMAIL CONFIRMATION ////////////////////////////////

                // Generate Email Confirmation Token for the User.
                // This code is send to the User' Email for confirmation
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);


                // create confirmation link
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

                return RedirectToPage("./ConfirmEmailInfoAfterRegisteration");

                ////////////////////////////EMAIL CONFIRMATION ////////////////////////////////

            }
            else
            {
                user = existingUser;
            }



            // Adds an external UserLoginInfo to the specified
            // user in aspnetuserlogins table
            result = await _userManager.AddLoginAsync(user, externalLoginInfo);

            if (!result.Succeeded)
            {
                // if we reached this far, some error occured
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                LoginProvider = externalLoginInfo.LoginProvider;
                ReturnUrl = returnUrl;
                return Page();
            }


            // Signs in the specified user. create the cookie
            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User created an account using {Name} provider.", externalLoginInfo.LoginProvider);
            return LocalRedirect(returnUrl);

        }
    }
}
