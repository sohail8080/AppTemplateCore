﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginWith2faModel> _logger;

        public LoginWith2faModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginWith2faModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        // Property coming from previous page, preserved in variable
        // neeeded during post back, OnPost() capturing it as Action Parameter
        public bool RememberMe { get; set; }

        // neeeded during post back, OnPost() capturing it as Action Parameter
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Authenticator code")]
            public string TwoFactorCode { get; set; }

            [Display(Name = "Remember this machine")]
            public bool RememberMachine { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            // UserName & Password is correct.
            // We come to this View while in the Login Process.
            // We redirected to this page after providing Valid UserName and Password.
            // WE ARE NOT SENDING ANY CALL/CODE TO THE AUTHENTICATOR APPLICATION.
            // HOW THE APPLICATION WILL KNOW THAT USER IS GOING TO LOGIN AND IT NEED 
            // TO PROVIDE A **AUTHENTICATOR CODE** THE USER.

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;
            RememberMe = rememberMe;

            return Page();
        }


        // loging in the user under 2FA.
        // after verifing UserName & Password, User comes to this screen
        // Provide the Authenticator Code provided by Authentor App & Verify that 
        // this code is Valid. 
        // HOW OUR APPLICATION GOING TO KNOW THAT PROVIDED AUTHENTICATOR CODE IS VALID.
        // DOES APPLICATION GO THE AUTHENTICATOR APP TO GET THAT AUTHENTICATOR CODE
        // DOES BOTH OUR APPLICATION & AUTHENTICATOR APPLICATION TAKE AUTHENTICATOR CODE FORM 
        // SHARED PLACE ON WEB/LIBRARY/ALGORITHM
        // 
        public async Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            // Validates the AUTHENTICATOR CODE/SignInCode from an authenticator app 
            // and creates and signs in the user
            // HOW OUR APPLICATION GOING TO KNOW THAT PROVIDED CODE IS VALID.
            // DOES APPLICATION GO THE AUTHENTICATOR APP TO GET THAT CODE
            // DOES BOTH OUR APPLICATION & AUTHENTICATOR APPLICATION TAKE CODE FORM 
            // SHARED PLACE
            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
                return LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Page();
            }
        }
    }
}
