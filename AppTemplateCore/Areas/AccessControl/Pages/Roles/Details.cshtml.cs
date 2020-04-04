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
using System.ComponentModel.DataAnnotations;

namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        // Controller dependencies
        private readonly ApplicationDbContext Context;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ILogger<DetailsModel> Logger;

        public DetailsModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DetailsModel> logger)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }
        private readonly string Success_Msg = "Successfully created new Role : {0}";
        private readonly string Error_Msg = "Error occurred while creating new Role : {0}";



        public InputModel Input { get; set; }


        public class InputModel : ApplicationRole
        {
            // Get the list of Users in this Role
            public IList<ApplicationUser> SelectedUserList { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { return NotFound(); }
            
            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            { return NotFound(); }

            Input = new InputModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            Input.SelectedUserList = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user, role.Name))
                {
                    Input.SelectedUserList.Add(user);
                }
            }

           
            return Page();
        }
    }
}
