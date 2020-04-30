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
    public class EditModel2 : UserPageModel
    {

        public EditModel2(
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

        }

        [Display(Name = "User Name")]
        public string Username { get; set; }

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
        
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            // ViewModel Property filling
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            await Load_Page_Reference_Data(user);

            // Show Page
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(string[] SelectedRoles, string[] SelectedClaims)
        {
            if (string.IsNullOrEmpty(Input.Id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, Input.Id); return NotFound(); }

            var user = await UserManager.FindByIdAsync(Input.Id);

            if (user == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, Input.Id); return NotFound(); }

            if (!ModelState.IsValid)
            {
                await Load_Page_Reference_Data_OnPost_Failed(user, SelectedRoles, SelectedClaims);
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
                Handle_Error_Response(result);
                await Load_Page_Reference_Data_OnPost_Failed(user, SelectedRoles, SelectedClaims);
                return Page();
            }

            var Is_Any_Role_Selected = (SelectedRoles != null && SelectedRoles.Length > 0);
            
            if (Is_Any_Role_Selected)
            {
                var Existing_Roles = await UserManager.GetRolesAsync(user);
                var Selected_Roles = SelectedRoles; 
                var Newly_Selected_Roles = Selected_Roles.Except(Existing_Roles).ToArray<string>();
                var Un_Selected_Roles = Existing_Roles.Except(Selected_Roles).ToArray<string>();

                // Only add newly added roles
                result = await UserManager.AddToRolesAsync(user, Newly_Selected_Roles);

                if (!result.Succeeded)
                {                    
                    Handle_Error_Response(result);
                    await Load_Page_Reference_Data_OnPost_Failed(user, SelectedRoles, SelectedClaims);
                    return Page();
                }
                else
                {
                    // Remove all roles other than selected roles.                   
                    result = await UserManager.RemoveFromRolesAsync(user, Un_Selected_Roles);
                    
                    if (!result.Succeeded)
                    {
    
                        Handle_Error_Response(result);
                        await Load_Page_Reference_Data_OnPost_Failed(user, SelectedRoles, SelectedClaims);
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

                    Handle_Error_Response(result);
                    await Load_Page_Reference_Data_OnPost_Failed(user, SelectedRoles, SelectedClaims);
                    return Page();
                }

            }            

            var Is_Any_Claim_Selected = (SelectedClaims != null && SelectedClaims.Length > 0);

            if (Is_Any_Claim_Selected)
            {
                var Existing_Claims = await UserManager.GetClaimsAsync(user);
                var Selected_Claims = ClaimsStore.AllClaims.Where(c => SelectedClaims.Contains(c.Value)).ToList(); 
                var Newly_Selected_Claims = Difference_Of_Claims_Lists(Selected_Claims, Existing_Claims);                
                var Un_Selected_Claims = Difference_Of_Claims_Lists(Existing_Claims, Selected_Claims);                

                result = await UserManager.AddClaimsAsync(user, Newly_Selected_Claims);

                if (!result.Succeeded)
                {   
                    Handle_Error_Response(result);
                    await Load_Page_Reference_Data_OnPost_Failed(user, SelectedRoles, SelectedClaims);
                    return Page();
                }
                else
                {
                    // Remove all un selected claims.                    
                    result = await UserManager.RemoveClaimsAsync(user, Un_Selected_Claims);
                    
                    if (!result.Succeeded)
                    {
                        Handle_Error_Response(result);
                        await Load_Page_Reference_Data_OnPost_Failed(user, SelectedRoles, SelectedClaims);
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
                    Handle_Error_Response(result);
                    await Load_Page_Reference_Data_OnPost_Failed(user, SelectedRoles, SelectedClaims);
                    return Page();
                }

            }



            var deptClaim = (await UserManager.GetClaimsAsync(user)).FirstOrDefault(dc => dc.Type == ClaimsStore.Department);

            if (deptClaim != null)
            {
                result = await UserManager.RemoveClaimAsync(user, deptClaim);

                if (!result.Succeeded)
                {
                    Handle_Error_Response(result);
                    return Page();
                }
            }

            if (!string.IsNullOrEmpty(Input.Department))
            {
                deptClaim = new Claim(ClaimsStore.Department, Input.Department);
                result = await UserManager.AddClaimAsync(user, deptClaim);

                if (!result.Succeeded)
                {
                    Handle_Error_Response(result);
                    return Page();
                }

            }




            Handle_Success_Response(result);            
            return RedirectToPage("./Index");

        }

        // approaches to find differences
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


        private async Task<bool> Load_Page_Reference_Data(ApplicationUser user)
        {
            Username = user.UserName;

            var userRoles = await UserManager.GetRolesAsync(user);
            var userClaims = await UserManager.GetClaimsAsync(user);

            var depClaim = userClaims.SingleOrDefault(uc => uc.Type == ClaimsStore.Department);

            Input = new InputModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Department = depClaim?.Value ?? string.Empty
            };


            var All_Roles = await RoleManager.Roles.ToListAsync();

            AllRolesList = All_Roles.Select(role => new UserHasRoles()
            {
                IsSelected = userRoles.Contains(role.Name),
                RoleId = role.Id,
                RoleName = role.Name
            }).ToList();

            AllClaimsList = ClaimsStore.AllClaims.Select(claim => new UserHasClaims()
            {
                IsSelected = userClaims.Any(uc => uc.Type == claim.Type && uc.Value == claim.Value),
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            }).ToList();


            return true;
        }

        private async Task<bool> Load_Page_Reference_Data_OnPost_Failed(ApplicationUser user, string[] SelectedRoles, string[] SelectedClaims)
        {
            Username = user.UserName;

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


        private void Handle_Success_Response(IdentityResult result)
        {
            Logger.LogInformation(string.Format(Edit_Success_Msg, Input.Email));
            StatusMessage = string.Format(Edit_Success_Msg, Input.Email);
        }

        private void Handle_Error_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Edit_Failed_Msg, Input.Email));
            StatusMessage = string.Format(Edit_Failed_Msg, Input.Email);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }
    }
}