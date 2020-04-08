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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.AccessControl.Pages.Users
{
    public class EditModel3 : UserPageModel
    {
        
        public EditModel3(
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

            // Inside
            public List<SelectListItem> AllRolesList { get; set; }
            public List<SelectListItem> AllClaimsList { get; set; }

        }

        [Display(Name = "User Name")]
        public string Username { get; set; }
        
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
                Handle_Error_Response(result);
                await Load_Form_Reference_Data_OnPost_Failed(user);
                return Page();
            }

            var Is_Any_Role_Selected = Input.AllRolesList.Any(r => r.Selected == true);

            // New User Added Successfully now add it roles
            if (Is_Any_Role_Selected)
            {
                var Existing_Roles = await UserManager.GetRolesAsync(user);
                var Selected_Roles = Input.AllRolesList.Where(r => r.Selected == true).Select(s => s.Text).ToList().ToArray();
                var Newly_Selected_Roles = Selected_Roles.Except(Existing_Roles).ToArray<string>();
                var Un_Selected_Roles = Existing_Roles.Except(Selected_Roles).ToArray<string>();

                // Only add newly added roles, do not add already added roles.                
                result = await UserManager.AddToRolesAsync(user, Newly_Selected_Roles);

                if (!result.Succeeded)
                {
                    // Error occurs while adding roles
                    //ViewBag.Message = "Error occurred while adding Record(s)";
                    Handle_Error_Response(result);
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
    
                        Handle_Error_Response(result);
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

                    Handle_Error_Response(result);
                    await Load_Form_Reference_Data_OnPost_Failed(user);
                    return Page();
                }
            }
            // here check for the case when all roles are un checked while edit

            var Is_Any_Claim_Selected = Input.AllClaimsList.Any(c => c.Selected == true);

            if (Is_Any_Claim_Selected)
            {
                var Existing_Claims = await UserManager.GetClaimsAsync(user);
                var Selected_Claims = Input.AllClaimsList.Where(c => c.Selected == true).Select(s => new Claim(s.Text, s.Value)).ToList();
                var Newly_Selected_Claims = Difference_Of_Claims_Lists(Selected_Claims, Existing_Claims);
                var Un_Selected_Claims = Difference_Of_Claims_Lists(Existing_Claims, Selected_Claims);


                result = await UserManager.AddClaimsAsync(user, Newly_Selected_Claims);

                if (!result.Succeeded)
                {   // Error occurs while adding claims                                                     
                    Handle_Error_Response(result);
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
    
                        Handle_Error_Response(result);
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

                    Handle_Error_Response(result);
                    await Load_Form_Reference_Data_OnPost_Failed(user);
                    return Page();
                }
            }

            Handle_Success_Response(result);
            // Show List Page
            return RedirectToPage("./Index");

        }

        private async Task<bool> Load_Form_Reference_Data(ApplicationUser user)
        {
            Username = user.UserName;

            var userRoles = await UserManager.GetRolesAsync(user);
            var userClaims = await UserManager.GetClaimsAsync(user);

            Input = new InputModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            var All_Roles = await RoleManager.Roles.ToListAsync();

            Input.AllRolesList = All_Roles.Select(role => new SelectListItem()
            {
                Selected = userRoles.Contains(role.Name),
                Value = role.Id,
                Text = role.Name
            }).ToList();

            Input.AllClaimsList = ClaimsStore.AllClaims.Select(claim => new SelectListItem()
            {
                Selected = userClaims.Any(uc => uc.Value == claim.Value),
                Text = claim.Type,
                Value = claim.Value,
            }).ToList();

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

        private void Handle_Success_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Edit_Success_Msg, Input.Email));
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