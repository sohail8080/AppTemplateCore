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
    public class LoginWithRecoveryCodeModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginWithRecoveryCodeModel> _logger;

        public LoginWithRecoveryCodeModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginWithRecoveryCodeModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        // Middle Variable that HOLD value when the form is rendered in GET operation
        // this Value is neeeded during th Post Operation when form is submitted.
        // Preserring Action parameter in variable, needed during post back
        // neeeded during post back, OnPost() capturing it as Action Parameter
        public string ReturnUrl { get; set; }

        // Only info related to Controls that is posted back is 
        // caputured in input model
        public class InputModel
        {
            [BindProperty]
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
        }

        // Ensure the user has gone through the username & password screen first
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            // Populate the Middle Man Varaible needed in post operation
            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Gets the User for the current two factor authentication login
            // User for the sign-in attempt.
            // Get the user that is currently trying to Login under 2FA
            // HOW SYSTEM KNOW ABOUT THAT USER. THERE COULD BE MANY?????
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            // REMOVE SPACES FROM RECOVER CODE.
            var recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty);

            // Signs in the user without two factor authentication using 
            // a two factor recovery code.
            // recoveryCode: The two factor recovery code.
            // Success, Failed, LockedOut, NotAllowed, TwoFactorRequired
            // Succeeded, IsLockedOut, IsNotAllowed, RequiresTwoFactor
            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToPage("./Lockout");
            }
            else
            {
                // Invalid Rec. Codes
                _logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return Page();
            }
        }
    }
}
