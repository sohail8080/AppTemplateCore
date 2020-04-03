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
    public class CreateModel2 : PageModel
    {
        // Controller dependencies
        private readonly ApplicationDbContext Context;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ILogger<CreateModel> Logger;

        // Controller Contructor initializing Controller dependencies by 
        //DI Container

        public CreateModel2(
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


        // ViewModel Properties
        // During OnGet() it will be blank
        // During OnPost() it will be filled by automatic model binding
        [BindProperty]
        public InputModel Input { get; set; }


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

        public List<UserHasRoles> AllRolesList { get; set; }
        public List<UserHasClaims> AllClaimsList { get; set; }

        public class UserHasRoles
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool IsSelected { get; set; }

        }

        public class UserHasClaims
        {
            public string ClaimType { get; set; }
            public string ClaimValue { get; set; }
            public bool IsSelected { get; set; }

        }

        // Just show the blank page
        // ViewModel Properties are used to render View
        public async Task<IActionResult> OnGetAsync()
        {
            await Load_Form_Reference_Data();
            return Page();
        }


        // OnPost(), ViewModel Properties filled and available
        // No need to catch then in Controller Action paramters
        public async Task<IActionResult> OnPostAsync(string[] SelectedRoles, string[] SelectedClaims)
        {
            // Model is VM Prperties
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

            // Use UserManager Service Class for storing User
            var result = await UserManager.CreateAsync(user, Input.Password);

            if (!result.Succeeded)
            {
                // Error occures while Adding New User                                            
                Add_Model_Errors(result);
                await Load_Form_Reference_Data_OnPost_Failed(SelectedRoles, SelectedClaims);
                return Page();
            }

            var Is_Any_Role_Selected = SelectedRoles != null && SelectedRoles.Length > 0;

            // New User Added Successfully now add it roles
            if (Is_Any_Role_Selected)
            {                

                // If some roles are selected for New User, Add those roles
                result = await UserManager.AddToRolesAsync(user, SelectedRoles);

                if (!result.Succeeded)
                {
                    // Error occurs while adding roles
                    Add_Model_Errors(result);
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
                {   // Error occurs while adding claims                                                     
                    Add_Model_Errors(result);
                    await Load_Form_Reference_Data_OnPost_Failed(SelectedRoles, SelectedClaims);
                    return Page();
                }
            }

            //ViewBag.Message = "Record(s) addded successfully.";
            // Show List Page
            Logger.LogInformation($"User {user.UserName} is created successfully.");
            return RedirectToPage("./Index");

        }


        private async Task<bool> Load_Form_Reference_Data()
        {
            Input = new InputModel();

            var All_Roles = await RoleManager.Roles.ToListAsync();

            AllRolesList = All_Roles.Select(x => new UserHasRoles()
            {
                IsSelected = false,
                RoleId = x.Id,
                RoleName = x.Name
            }).ToList();

            AllClaimsList = ClaimsStore.AllClaims.Select(x => new UserHasClaims()
            {
                IsSelected = false,
                ClaimType = x.Type,
                ClaimValue = x.Value,
            }).ToList();

            return true;

        }



        private async Task<bool> Load_Form_Reference_Data_OnPost_Failed(string[] SelectedRoles, string[] SelectedClaims)
        {
            
            var All_Roles = await RoleManager.Roles.ToListAsync();

            AllRolesList = All_Roles.Select(role => new UserHasRoles()
            {
                IsSelected = SelectedRoles.Contains(role.Name),
                RoleId = role.Id,
                RoleName = role.Name
            }).ToList();

            AllClaimsList = ClaimsStore.AllClaims.Select(claim => new UserHasClaims()
            {
                IsSelected = SelectedClaims.Contains(claim.Value),
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            }).ToList();

            return true;

        }



        private void Add_Model_Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }

       
    }
}