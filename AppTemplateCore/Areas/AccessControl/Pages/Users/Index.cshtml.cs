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

        // Public VM Properties to show List of Application Roles
        // Domain Model Classes are OK for Display purpose if they are fit to scena
        //public IList<ApplicationUser> ApplicationUser { get; set; }

        public IList<UserWithRoles> UsersWithRoles { get; set; }


        public class UserWithRoles : ApplicationUser
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

            UsersWithRoles = new List<UserWithRoles>();

            foreach (var user in users)
            {
                var userWithRoles = new UserWithRoles();
                userWithRoles.Id = user.Id;
                userWithRoles.UserName = user.UserName;
                userWithRoles.Email = user.Email;

                userWithRoles.FirstName = user.FirstName;
                userWithRoles.LastName = user.LastName;
                userWithRoles.PhoneNumber = user.PhoneNumber;

                userWithRoles.RolesList = await UserManager.GetRolesAsync(user);

                UsersWithRoles.Add(userWithRoles);
            }


        }


    }
}