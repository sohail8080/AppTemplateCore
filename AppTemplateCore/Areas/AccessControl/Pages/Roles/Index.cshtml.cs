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
    public class IndexModel : PageModel
    {
        // Controller Dependencies
        // Controller dependencies
        private readonly ApplicationDbContext Context;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ILogger<IndexModel> Logger;

        // Controller Constructors, loading Controller Dependencies
        public IndexModel(
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

        // Public VM Properties to show List of Application Roles
        // Domain Model Classes are OK for Display purpose if they are fit to scena
        //public IList<ApplicationRole> ApplicationRole { get;set; }


        public IList<InputModel> Input { get; set; }


        public class InputModel : ApplicationRole
        {
            // Get the list of Users in this Role
            public IList<ApplicationUser> UserList { get; set; }
        }



        // Fill the ViewModel Properties, that all, no need to call Page();
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

            //ApplicationRole = await Context.Roles.Include(u => u.Users).ToListAsync();
        }


        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { return NotFound(); }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
                //ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";                
            }

            IdentityResult result = await RoleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                //ViewBag.Message = "Error occurred while deleting Record(s)";
                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }

            //ViewBag.Message = "Record(s) deleted successfully.";
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
                //ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";                
            }

            IdentityResult result = await RoleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                //ViewBag.Message = "Error occurred while deleting Record(s)";
                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }

            //ViewBag.Message = "Record(s) deleted successfully.";
            Logger.LogInformation($"Role {role.Name} is deleted successfully.");

            return RedirectToPage("./Index");

        }
    }
}
