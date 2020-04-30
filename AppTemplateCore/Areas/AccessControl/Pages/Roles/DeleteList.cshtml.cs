using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{    

    //Multiple Roles Delete Operation by Check Box List in One Step

    public class DeleteListModel : RolePageModel
    {
        
        public DeleteListModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<IndexModel> logger)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Logger = logger;
        }

        [BindProperty]
        public IList<InputModel> Input { get; set; }

        
        public class InputModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public bool IsSelected { get; set; }

            // Get the list of Users in this Role
            public IList<ApplicationUser> UserList { get; set; }
        }
        
        public async Task OnGetAsync()
        {
            var roles = await RoleManager.Roles.ToListAsync();

            Input = new List<InputModel>();

            foreach (var role in roles)
            {
                var roleWithUsers = new InputModel();
                roleWithUsers.Id = role.Id;
                roleWithUsers.Name = role.Name;
                roleWithUsers.IsSelected = false;
                roleWithUsers.UserList = await UserManager.GetUsersInRoleAsync(role.Name);
                Input.Add(roleWithUsers);
            }
            
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var Is_Any_Role_Selected = Input.Any(role => role.IsSelected == true);

            if (!Is_Any_Role_Selected)
                return Page();

            IdentityResult result = IdentityResult.Success;

            foreach (var role in Input)
            {
                
                if (role.IsSelected)
                {
                    var appRole = await RoleManager.FindByIdAsync(role.Id);
                    result = await RoleManager.DeleteAsync(appRole);
                }

                if (!result.Succeeded)
                {
                    Handle_Error_Response(result);                  
                    return Page();
                }
            }

            Handle_Success_Response(result);
            return Page();
        }


        private void Handle_Success_Response(IdentityResult result)
        {
            Logger.LogInformation(string.Format(Delete_All_Success_Msg));
            StatusMessage = string.Format(Delete_All_Success_Msg);
        }

        private void Handle_Error_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Delete_All_Failed_Msg));
            StatusMessage = string.Format(Delete_All_Failed_Msg);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }



    }
}
