using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.AccessControl.Pages.Users
{    
    public class DeleteModel : UserPageModel
    {

        public DeleteModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeleteModel> logger)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }


        public class InputModel
        {

            [Required]
            public string Id { get; set; }

            [Required]
            [Display(Name = "First Name")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "User Name")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string UserName { get; set; }


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
            [Display(Name = "Department")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            public string Department { get; set; }

            // Get the list of Users in this Role
            public IList<string> SelectedRolesList { get; set; }
            public IList<Claim> SelectedClaimsList { get; set; }

        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            await Load_Page_Reference_Data(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, Input.Id); return NotFound(); }

            var user = await UserManager.FindByIdAsync(Input.Id);

            if (user == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, Input.Id); return NotFound(); }

            IdentityResult result = await UserManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                Handle_Error_Response(result);
                await Load_Page_Reference_Data(user);
                return Page();
            }

            Handle_Success_Response(result);

            return RedirectToPage("./Index");
        }

        private async Task<bool> Load_Page_Reference_Data(ApplicationUser user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);
            var userClaims = await UserManager.GetClaimsAsync(user);
            var depClaim = userClaims.SingleOrDefault(uc => uc.Type == ClaimsStore.Department);

            Input = new InputModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Department = depClaim?.Value ?? string.Empty,
                SelectedRolesList = userRoles,
                SelectedClaimsList = userClaims
            };

            return true;
        }


        private void Handle_Success_Response(IdentityResult result)
        {
            Logger.LogInformation(string.Format(Delete_Success_Msg, Input.Email));
            StatusMessage = string.Format(Delete_Success_Msg, Input.Email);
        }

        private void Handle_Error_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Delete_Failed_Msg, Input.Email));
            StatusMessage = string.Format(Delete_Failed_Msg, Input.Email);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }


    }
}