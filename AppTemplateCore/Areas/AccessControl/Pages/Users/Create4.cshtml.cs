using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.AccessControl.Pages.Users
{
    public class CreateModel4 : UserPageModel
    {

        public CreateModel4(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<CreateModel> logger)
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

        public List<SelectListItem> AllRolesList { get; set; }
        public List<SelectListItem> AllClaimsList { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            await Load_Form_Reference_Data();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(string[] SelectedRoles, string[] SelectedClaims)
        {            
            if (!ModelState.IsValid)
            {
                await Load_Form_Reference_Data_OnPost_Failed(SelectedRoles, SelectedClaims);
                return Page();
            }

            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName
            };
            
            var result = await UserManager.CreateAsync(user, Input.Password);

            if (!result.Succeeded)
            {                
                Handle_Error_Response(result);
                await Load_Form_Reference_Data_OnPost_Failed(SelectedRoles, SelectedClaims);
                return Page();
            }

            var Is_Any_Role_Selected = SelectedRoles != null && SelectedRoles.Length > 0;
            
            if (Is_Any_Role_Selected)
            {                                
                result = await UserManager.AddToRolesAsync(user, SelectedRoles);

                if (!result.Succeeded)
                {                    
                    Handle_Error_Response(result);
                    await Load_Form_Reference_Data_OnPost_Failed(SelectedRoles, SelectedClaims);
                    return Page();
                }
            }

            var Is_Any_Claim_Selected = SelectedClaims != null && SelectedClaims.Length > 0;

            if (Is_Any_Claim_Selected)
            {

                List<Claim> Selected_Claims = ClaimsStore.AllClaims
                   .Where(claim => SelectedClaims.Contains(claim.Value)).ToList();

                result = await UserManager.AddClaimsAsync(user, Selected_Claims);

                if (!result.Succeeded)
                {   
                    Handle_Error_Response(result);
                    await Load_Form_Reference_Data_OnPost_Failed(SelectedRoles, SelectedClaims);
                    return Page();
                }
            }

            Handle_Success_Response(result);
            return RedirectToPage("./Index");

        }


        private async Task<bool> Load_Form_Reference_Data()
        {
            Input = new InputModel();

            var All_Roles = await RoleManager.Roles.ToListAsync();

            AllRolesList = All_Roles.Select(role => new SelectListItem()
            {
                Selected = false,
                Value = role.Id,
                Text = role.Name
            }).ToList();

            AllClaimsList = ClaimsStore.AllClaims.Select(claim => new SelectListItem()
            {
                Selected = false,
                Text = claim.Type,
                Value = claim.Value,
            }).ToList();

            return true;

        }



        private async Task<bool> Load_Form_Reference_Data_OnPost_Failed(string[] SelectedRoles, string[] SelectedClaims)
        {
            
            var All_Roles = await RoleManager.Roles.ToListAsync();

            AllRolesList = All_Roles.Select(role => new SelectListItem()
            {
                Selected = SelectedRoles.Contains(role.Name),
                Value = role.Id,
                Text = role.Name
            }).ToList();

            AllClaimsList = ClaimsStore.AllClaims.Select(claim => new SelectListItem()
            {
                Selected = SelectedClaims.Contains(claim.Value),
                Text = claim.Type,
                Value = claim.Value,
            }).ToList();

            return true;

        }



        private void Add_Model_Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }


        private void Handle_Success_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Create_Success_Msg, Input.Email));
            StatusMessage = string.Format(Create_Success_Msg, Input.Email);
        }

        private void Handle_Error_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Create_Failed_Msg, Input.Email));
            StatusMessage = string.Format(Create_Failed_Msg, Input.Email);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }


    }
}