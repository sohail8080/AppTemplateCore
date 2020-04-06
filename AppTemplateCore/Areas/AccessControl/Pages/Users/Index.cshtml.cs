using System;
using System.Collections.Generic;
using System.Linq;
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


        [TempData]
        public string StatusMessage { get; set; }
        private readonly string Success_Msg = "Successfully deleted new Role : {0}";
        private readonly string Error_Msg = "Error occurred while deleting new Role : {0}";


        // Public VM Properties to show List of Application Roles
        // Domain Model Classes are OK for Display purpose if they are fit to scena
        //public IList<ApplicationUser> ApplicationUser { get; set; }

        public IList<InputModel> Input { get; set; }


        public class InputModel : ApplicationUser
        {
            // Get the list of Users in this Role
            public IList<string> RolesList { get; set; }
        }


        // Fill the ViewModel Properties, that all, no need to call Page();
        public async Task OnGetAsync()
        {
            //ApplicationUser = await Context.Users.ToListAsync();
            //var users = UserManager.Users.ToList();

            var users = await UserManager.Users.ToListAsync();

            Input = new List<InputModel>();

            foreach (var user in users)
            {
                var userWithRoles = new InputModel();
                userWithRoles.Id = user.Id;
                userWithRoles.UserName = user.UserName;
                userWithRoles.Email = user.Email;

                userWithRoles.FirstName = user.FirstName;
                userWithRoles.LastName = user.LastName;
                userWithRoles.PhoneNumber = user.PhoneNumber;

                userWithRoles.RolesList = await UserManager.GetRolesAsync(user);

                Input.Add(userWithRoles);
            }


        }


        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { return NotFound(); }

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
                                
            }

            IdentityResult result = await UserManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                
                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }

            
            Logger.LogInformation($"Role {user.UserName} is deleted successfully.");

            return RedirectToPage("./Index");

        }


        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { return NotFound(); }

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
                                
            }

            IdentityResult result = await UserManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                
                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }

            
            Logger.LogInformation($"Role {user.UserName} is deleted successfully.");

            return RedirectToPage("./Index");

        }


        private void Handle_Success_Response(IdentityResult result, ApplicationUser user)
        {
            Logger.LogError(string.Format(Success_Msg, user.Email));
            StatusMessage = string.Format(Success_Msg, user.Email);
        }

        private void Handle_Error_Response(IdentityResult result, ApplicationUser user)
        {
            Logger.LogError(string.Format(Error_Msg, user.Email));
            StatusMessage = string.Format(Error_Msg, user.Email);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }



    }
}