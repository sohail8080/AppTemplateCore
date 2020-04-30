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
    //Edit Roles on this screen, Edit User on Edit5 Screen
    // Seperation of 3 Edit operations on 3 Screens

    public class AddRolesToUserModel : UserPageModel
    {

        public AddRolesToUserModel(
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

            [Display(Name = "User Name")]
            public string Username { get; set; }

            public List<UserHasRoles> AllRolesList { get; set; }

        }


        public class UserHasRoles
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool IsSelected { get; set; }

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

            //if (!ModelState.IsValid)
            //{
            //    await Load_Page_Reference_Data_OnPost_Failed(user);
            //    return Page();
            //}

            IdentityResult result = null;

            var Is_Any_Role_Selected = Input.AllRolesList.Any(r => r.IsSelected == true);

            if (Is_Any_Role_Selected)
            {
                var Existing_Roles = await UserManager.GetRolesAsync(user);
                var Selected_Roles = Input.AllRolesList.Where(r => r.IsSelected == true).Select(s => s.RoleName).ToList().ToArray();
                var Newly_Selected_Roles = Selected_Roles.Except(Existing_Roles).ToArray<string>();
                var Un_Selected_Roles = Existing_Roles.Except(Selected_Roles).ToArray<string>();

                // Only add newly added roles
                result = await UserManager.AddToRolesAsync(user, Newly_Selected_Roles);

                if (!result.Succeeded)
                {
                    Handle_Error_Response(result);
                    //await Load_Page_Reference_Data_OnPost_Failed(user);
                    return Page();
                }

                // Remove all roles other than selected roles.                   
                result = await UserManager.RemoveFromRolesAsync(user, Un_Selected_Roles);

                if (!result.Succeeded)
                {
                    Handle_Error_Response(result);
                    //await Load_Page_Reference_Data_OnPost_Failed(user);
                    return Page();
                }
            }
            else
            {
                // remove all existing roles
                result = await UserManager.RemoveFromRolesAsync(user, await UserManager.GetRolesAsync(user));

                if (!result.Succeeded)
                {
                    Handle_Error_Response(result);
                    //await Load_Page_Reference_Data_OnPost_Failed(user);
                    return Page();
                }
            }


            Handle_Success_Response(result);
            return RedirectToPage("./Edit5", routeValues: new { id = Input.Id });

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

        private async Task<bool> Load_Page_Reference_Data(ApplicationUser user)
        {


            var userRoles = await UserManager.GetRolesAsync(user);
            var userClaims = await UserManager.GetClaimsAsync(user);

            var depClaim = userClaims.SingleOrDefault(uc => uc.Type == ClaimsStore.Department);

            var All_Roles = await RoleManager.Roles.ToListAsync();

            Input = new InputModel
            {
                Id = user.Id,
                Username = user.UserName,

                AllRolesList = All_Roles.Select(role => new UserHasRoles()
                {
                    //IsSelected = userRoles.Any(ur => ur == role.Name),
                    IsSelected = userRoles.Contains(role.Name),
                    RoleId = role.Id,
                    RoleName = role.Name
                }).ToList(),

            };


            return true;
        }

        //private async Task<bool> Load_Page_Reference_Data_OnPost_Failed(ApplicationUser user)
        //{
        //    Username = user.UserName;
        //    return true;
        //}

        private void Add_Model_Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }

        private void Handle_Success_Response(IdentityResult result)
        {
            Logger.LogInformation(string.Format(Edit_Success_Msg, Input.Username));
            StatusMessage = string.Format(Edit_Success_Msg, Input.Username);
        }

        private void Handle_Error_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Edit_Failed_Msg, Input.Username));
            StatusMessage = string.Format(Edit_Failed_Msg, Input.Username);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }

    }
}