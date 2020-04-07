using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{
    public class CreateModel : RolePageModel
    {
        
        public CreateModel(
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
            [Required(AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
            [Display(Name = "Role Name")]
            public string Name { get; set; }
            
            public List<RoleHasUsers> AllUsersList { get; set; }

        }


        public class RoleHasUsers
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public bool IsSelected { get; set; }

        }

        public async Task<IActionResult> OnGetAsync()
        {
            await Load_Form_Reference_Data();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
           
            if (!ModelState.IsValid)
            {                
                return Page();
            }

            var role = new ApplicationRole() { Name = Input.Name };

            IdentityResult result = await RoleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                Handle_Error_Response(result);
                return Page();
            }

            var Is_Any_User_Selected = Input.AllUsersList.Any(user => user.IsSelected == true);

            
            if (Is_Any_User_Selected)
            {
                //var Selected_Users = Input.AllUsersList.Where(user => user.IsSelected == true).Select(s => s.UserName).ToList().ToArray();
                var Selected_Users = Input.AllUsersList.Where(user => user.IsSelected == true);

                foreach (var user in Selected_Users)
                {
                    var appUser = await UserManager.FindByIdAsync(user.UserId);
                    result = await UserManager.AddToRoleAsync(appUser, role.Name);

                    if (!result.Succeeded)
                    {                        
                        Handle_Error_Response(result);
                        return Page();
                    }

                }                
                               
                if (!result.Succeeded)
                {                    
                    Handle_Error_Response(result);
                    return Page();
                }
            }

            Handle_Success_Response(result);
            return RedirectToPage("./Index");

        }


        private async Task<bool> Load_Form_Reference_Data()
        {
            Input = new InputModel();

            var All_Users = await UserManager.Users.ToListAsync();

            Input.AllUsersList = All_Users.Select(user => new RoleHasUsers()
            {
                IsSelected = false,
                UserId = user.Id,
                UserName = user.UserName
            }).ToList();

            return true;
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

    }
}