using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class RegisterModel : PageModel
    {
        // View/Controller Dependencies for Post & Get
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        // Dependencies will be provided by DI/IOC
        // Interfaces for the Dependenceis
        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        // FormData or PostBack Data 
        [BindProperty]
        public InputModel Input { get; set; }

        // Used in the Post Operation
        // OnGet() will preserve it Action paramter inside this varaible
        // Form will hold this in Form Tage as Route Parameter
        // OnPost() will again get this as ActionParameter.
        // This is not FormData, but it is RouteData stored in Local Variable
        public string ReturnUrl { get; set; }

        // This Model need to be Validated on POST
        // This Model is used to Render the View on GET
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "First Name")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string LastName { get; set; }

        }

        // explicityly setting the pimiitve type of default value 
        // we are getting primitive type binded to the Request Data
        // This Screen is called by Login-Screen & Login Partial Screen
        // Retrun URL is perserved, needed in post Operation
        // Put Return URL in ViewModel and Render it on the View
        // then get it in the Form Action and send to the following 
        // post operation.
        // It is not transferred as it is done in simple class. 
        // Both Get and Post methods are called in two different web request.
        // Both of these calls are statless.
        // Value Availble in Get is put in the Instance Variable of ViewModel
        //
        public void OnGet(string returnUrl = null)
        {
            // Perserved RouteData as it is needed in OnPost()
            ReturnUrl = returnUrl;
        }


        // explicityly setting the pimiitve type of default value 
        // we are getting primitive type binded to the Request Data
        // User wants to register with the site, Get the User Profile
        // Save Profile DAta in DB, Send Email, and as for Email Verification
        // Do not signg in user. If Email confirmation is not the part of 
        // SignIn Workflow, then SignIN the User right after Registeration.
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // if returnurl is null, set to the root of the application
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                // create the user
                var user = new ApplicationUser {

                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                };

                // put new user in the db
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    // When System State in DB changed, then log that info
                    // Log() Formats and writes a log message at the specified log level.
                    // LogCritical()
                    // LogDebug()
                    // LogError()
                    // LogInformation()
                    // LogTrace()
                    // LogWarning()
                    _logger.LogInformation("User created a new account with password.");

                    // Generate Email Confirmation Token for the User.
                    // This code is send to the User' Email for confirmation
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    // UserId & EmailConfirmationToken is included in the Email Confirmation Link
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    // Send Email to the users email.
                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // Sign in the user to the System. This should not be here.
                    // User should be signed in once he Confirms the Email.
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
