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

namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{
    public class IndexModel2 : RolePageModel
    {
        // Controller Constructors, loading Controller Dependencies
        public IndexModel2(
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

        public IList<InputModel> Input { get; set; }


        public class InputModel : ApplicationRole
        {
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
                roleWithUsers.NormalizedName = role.NormalizedName;
                roleWithUsers.UserList = await UserManager.GetUsersInRoleAsync(role.Name);
                Input.Add(roleWithUsers);
            }
           
        }


        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { return NotFound(); }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
                                
            }

            IdentityResult result = await RoleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                
                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }

            
            Logger.LogInformation($"Role {role.Name} is deleted successfully.");

            return RedirectToPage("./Index");

        }


        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { return NotFound(); }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
                                
            }

            IdentityResult result = await RoleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                
                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }

            
            Logger.LogInformation($"Role {role.Name} is deleted successfully.");

            return RedirectToPage("./Index");

        }



    }
}
