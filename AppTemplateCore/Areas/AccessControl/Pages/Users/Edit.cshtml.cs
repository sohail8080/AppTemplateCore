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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.AccessControl.Pages.Users
{
    public class EditModel : PageModel
    {
        // Controller dependencies : UOW
        // Controller dependencies
        private readonly ApplicationDbContext Context;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ILogger<EditModel> Logger;

        // DI Container injects UOW inside controller constructor
        public EditModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<EditModel> logger)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Logger = logger;
        }


        [TempData]
        public string StatusMessage { get; set; }
        private readonly string Success_Msg = "Successfully created new Role : {0}";
        private readonly string Error_Msg = "Error occurred while creating new Role : {0}";


        // View Model Properties available in View
        // OnGet() we fill this property and show Page();
        // OnPost() we do not get this property as ActionParameters
        // When form is posted this property is Auto Filled and availble
        // ModelState works as before in Controller
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

            public List<UserHasRoles> AllRolesList { get; set; }
            public List<UserHasClaims> AllClaimsList { get; set; }

        }


        // Render the UI but not invovled in Post() 
        // We only want to show this field on form
        // But we do not want to Post Back this field
        // We do not want to get this field in OnPost()
        // We do not want Model Binding to happen on this
        [Display(Name = "User Name")]
        public string Username { get; set; }


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



        // OnGet(), fill ViewModel Propertis and show Page();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { return NotFound(); }

            // ViewModel Property filling
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            { return NotFound(); }

            await Load_Form_Reference_Data(user);

            // Show Page
            return Page();
        }


        // OnPost(), ViewModel Properties filled and available
        // No need to catch then in Controller Action paramters
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Id))
            { return NotFound(); }

            var user = await UserManager.FindByIdAsync(Input.Id);

            if (user == null)
            { return NotFound(); }

            // Here Model means all Properties of this class
            // VM Properties already filled, show page again
            if (!ModelState.IsValid)
            {
                await Load_Form_Reference_Data_OnPost_Failed(user);
                return Page();
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.Email = Input.Email;
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;

            IdentityResult result = await UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                //ViewBag.Message = "Error occurred while updating Record(s)";
                Add_Model_Errors(result);
                await Load_Form_Reference_Data_OnPost_Failed(user);
                return Page();
            }

            var Is_Any_Role_Selected = Input.AllRolesList.Any(r => r.IsSelected == true);

            // New User Added Successfully now add it roles
            if (Is_Any_Role_Selected)
            {
                var Existing_Roles = await UserManager.GetRolesAsync(user);
                var Selected_Roles = Input.AllRolesList.Where(r => r.IsSelected == true).Select(s => s.RoleName).ToList().ToArray();
                var Newly_Selected_Roles = Selected_Roles.Except(Existing_Roles).ToArray<string>();
                var Un_Selected_Roles = Existing_Roles.Except(Selected_Roles).ToArray<string>();

                // Only add newly added roles, do not add already added roles.                
                result = await UserManager.AddToRolesAsync(user, Newly_Selected_Roles);

                if (!result.Succeeded)
                {
                    // Error occurs while adding roles
                    //ViewBag.Message = "Error occurred while adding Record(s)";
                    Add_Model_Errors(result);
                    await Load_Form_Reference_Data_OnPost_Failed(user);
                    return Page();
                }
                else
                {
                    // Remove all roles other than selected roles.                   
                    result = await UserManager.RemoveFromRolesAsync(user, Un_Selected_Roles);

                    // Error occurs while removing roles, but user edited, role added, not removed
                    if (!result.Succeeded)
                    {
                        //ViewBag.Message = "Error occurred while updating Record(s)";
                        Add_Model_Errors(result);
                        await Load_Form_Reference_Data_OnPost_Failed(user);
                        return Page();
                    }
                }
            }
            else
            {
                // remove all existing roles
                result = await UserManager.RemoveFromRolesAsync(user, await UserManager.GetRolesAsync(user));

                if (!result.Succeeded)
                {
                    //ViewBag.Message = "Error occurred while updating Record(s)";
                    Add_Model_Errors(result);
                    await Load_Form_Reference_Data_OnPost_Failed(user);
                    return Page();
                }

            }
            // here check for the case when all roles are un checked while edit

            var Is_Any_Claim_Selected = Input.AllClaimsList.Any(c => c.IsSelected == true);

            if (Is_Any_Claim_Selected)
            {
                var Existing_Claims = await UserManager.GetClaimsAsync(user);
                var Selected_Claims = Input.AllClaimsList.Where(c => c.IsSelected == true).Select(s => new Claim(s.ClaimType, s.ClaimValue)).ToList();                
                var Newly_Selected_Claims = Difference_Of_Claims_Lists(Selected_Claims, Existing_Claims);                
                var Un_Selected_Claims = Difference_Of_Claims_Lists(Existing_Claims, Selected_Claims);                

                result = await UserManager.AddClaimsAsync(user, Newly_Selected_Claims);

                if (!result.Succeeded)
                {   // Error occurs while adding claims                                                     
                    Add_Model_Errors(result);
                    await Load_Form_Reference_Data_OnPost_Failed(user);
                    return Page();
                }
                else
                {
                    // Remove all un selected claims.                    
                    result = await UserManager.RemoveClaimsAsync(user, Un_Selected_Claims);

                    // Error occurs while removing claims, user edited, roles added, claims added but unchecked claims not removed
                    if (!result.Succeeded)
                    {
                        //ViewBag.Message = "Error occurred while updating Record(s)";
                        Add_Model_Errors(result);
                        await Load_Form_Reference_Data_OnPost_Failed(user);
                        return Page();
                    }
                }
            }
            else
            {
                // remove all existing user claims
                result = await UserManager.RemoveClaimsAsync(user, await UserManager.GetClaimsAsync(user));

                if (!result.Succeeded)
                {
                    //ViewBag.Message = "Error occurred while updating Record(s)";
                    Add_Model_Errors(result);
                    await Load_Form_Reference_Data_OnPost_Failed(user);
                    return Page();
                }

            }


            //ViewBag.Message = "Record(s) updated successfully.");
            Logger.LogInformation($"User {Username} is updated successfully.");
            // Show List Page
            return RedirectToPage("./Index");

        }

        private IList<Claim> Difference_Of_Claims_Lists
            (IList<Claim> Source_List, IList<Claim> Target_List)
        {

            // Ist difference Code Working
            return Source_List.Where(source =>
                    !Target_List.Any(target => target.Type == source.Type &&
                    target.Value == source.Value)).ToList();

            // 2nd difference Code Working
            //return Source_List.Where(source =>
            //Target_List.All(target => target.Type != source.Type || target.Value != source.Value)).ToList();

            // 3rd difference Code Working
            //List<Claim> difference = new List<Claim>();

            //foreach (var source in Source_List)
            //{
            //    bool matched = false;

            //    foreach (var target in Target_List)
            //    {
            //        if (source.Type == target.Type && source.Value == target.Value)
            //            matched = true;
            //    }

            //    if (!matched)
            //    { difference.Add(source); }
            //}

            //return difference;
        }

        private bool ApplicationUserExists(string id)
        {
            return Context.Users.Any(e => e.Id == id);
        }

        private async Task<bool> Load_Form_Reference_Data(ApplicationUser user)
        {
            Username = user.UserName;

            var userRoles = await UserManager.GetRolesAsync(user);
            var userClaims = await UserManager.GetClaimsAsync(user);

            var All_Roles = await RoleManager.Roles.ToListAsync();

            Input = new InputModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,

                AllRolesList = All_Roles.Select(role => new UserHasRoles()
                {
                    //IsSelected = userRoles.Any(ur => ur == role.Name),
                    IsSelected = userRoles.Contains(role.Name),
                    RoleId = role.Id,
                    RoleName = role.Name
                }).ToList(),

                AllClaimsList = ClaimsStore.AllClaims.Select(claim => new UserHasClaims()
                {                    
                    IsSelected = userClaims.Any(uc => uc.Value == claim.Value),
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value,
                }).ToList()
            };


            return true;
        }

        private async Task<bool> Load_Form_Reference_Data_OnPost_Failed(ApplicationUser user)
        {
            Username = user.UserName;
            return true;
        }

        private void Add_Model_Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }

        private void Handle_Success_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Success_Msg, Input.Email));
            StatusMessage = string.Format(Success_Msg, Input.Email);
        }

        private void Handle_Error_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Error_Msg, Input.Email));
            StatusMessage = string.Format(Error_Msg, Input.Email);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }

    }
}