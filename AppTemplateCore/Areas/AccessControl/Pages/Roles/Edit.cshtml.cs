using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;


namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{
    public class EditModel : PageModel
    {
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
        private readonly string Success_Msg = "Successfully modified Role : {0}";
        private readonly string Error_Msg = "Error occurred while modifying Role : {0}";


        // View Model Properties available in View
        // OnGet() we fill this property and show Page();
        // OnPost() we do not get this property as ActionParameters
        // When form is posted this property is Auto Filled and availble
        // ModelState works as before in Controller
        [BindProperty]
        public InputModel Input { get; set; }

        // Fit to purpose ViewModel should be used in case of Add/Edit
        public class InputModel
        {
            [Required(AllowEmptyStrings = false)]
            public string Id { get; set; }

            [Required(AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
            [Display(Name = "Role Name")]
            public string Name { get; set; }

            // Get the list of Users in this Role
            public List<RoleHasUsers> AllUsersList { get; set; }

        }

        public class RoleHasUsers
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public bool IsSelected { get; set; }
        }

        // OnGet(), fill ViewModel Propertis and show Page();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { return NotFound(); }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            { return NotFound(); }

            await Load_Form_Reference_Data(role);
            return Page();
        }

        // OnPost(), ViewModel Properties filled and available
        // No need to catch then in Controller Action paramters
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Id))
            { return NotFound(); }

            var role = await RoleManager.FindByIdAsync(Input.Id);

            if (role == null)
            { return NotFound(); }

            // Here Model means all Properties of this class
            if (!ModelState.IsValid)
            {
                await Load_Form_Reference_Data(role);
                return Page();
            }

            role.Name = Input.Name;

            IdentityResult result = await RoleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                Handle_Error_Response(result);
                await Load_Form_Reference_Data(role);
                return Page();
            }

            var Is_Any_User_Selected = Input.AllUsersList.Any(user => user.IsSelected == true);

            // New User Added Successfully now add it roles
            // If some users are selected for Role, Add those roles
            if (Is_Any_User_Selected)
            {

                List<string> Existing_Users_In_Role = new List<string>();
                foreach (var user in UserManager.Users)
                { if (await UserManager.IsInRoleAsync(user, role.Name))
                        Existing_Users_In_Role.Add(user.Id);
                }
                
                var Selected_Users = Input.AllUsersList.Where(user => user.IsSelected == true).Select(s => s.UserId).ToList().ToArray();
                var Newly_Selected_Users = Selected_Users.Except(Existing_Users_In_Role).ToArray<string>();
                var Un_Selected_Users = Existing_Users_In_Role.Except(Selected_Users).ToArray<string>();

                foreach (var user in Newly_Selected_Users)
                {
                    var appUser = await UserManager.FindByIdAsync(user);
                    result = await UserManager.AddToRoleAsync(appUser, role.Name);

                    if (!result.Succeeded)
                    {
                        // Error occurs while adding users
                        Handle_Error_Response(result);
                        return Page();
                    }

                }


                foreach (var user in Un_Selected_Users)
                {
                    var appUser = await UserManager.FindByIdAsync(user);
                    result = await UserManager.RemoveFromRoleAsync(appUser, role.Name);

                    if (!result.Succeeded)
                    {
                        // Error occurs while adding roles
                        Handle_Error_Response(result);
                        return Page();
                    }

                }

            }
            else
            {
                // Remove all Users of the Role
                List<string> Existing_Users_In_Role = new List<string>();
                foreach (var user in UserManager.Users)
                {
                    if (await UserManager.IsInRoleAsync(user, role.Name))
                        Existing_Users_In_Role.Add(user.Id);
                }


                foreach (var user in Existing_Users_In_Role)
                {
                    var appUser = await UserManager.FindByIdAsync(user);
                    result = await UserManager.RemoveFromRoleAsync(appUser, role.Name);

                    if (!result.Succeeded)
                    {
                        // Error occurs while adding roles
                        Handle_Error_Response(result);
                        return Page();
                    }

                }
                
            }

            Handle_Success_Response(result);
            return RedirectToPage("./Index");
        }

        private async Task<bool> Load_Form_Reference_Data(ApplicationRole role)
        {
            Input = new InputModel()
            {
                Id = role.Id,
                Name = role.Name
            };


            var All_Users = await UserManager.Users.ToListAsync();

            var Role_Users = UserManager.GetUsersInRoleAsync(role.Name).Result;

            Input.AllUsersList = All_Users.Select(user => new RoleHasUsers()
            {
                IsSelected = Role_Users.Contains(user),
                UserId = user.Id,
                UserName = user.UserName
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
            Logger.LogError(string.Format(Success_Msg, Input.Name));
            StatusMessage = string.Format(Success_Msg, Input.Name);
        }

        private void Handle_Error_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Error_Msg, Input.Name));
            StatusMessage = string.Format(Error_Msg, Input.Name);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }

        private bool ApplicationRoleExists(string id)
        {
            return Context.Roles.Any(e => e.Id == id);
        }
    }
}
