using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    public class DetailsModel : UserPageModel
    {

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

        // This Model need to be Validated on POST
        // This Model is used to Render the View on GET
        public InputModel Input { get; set; }


        public class InputModel : ApplicationUser
        {
            // Get the list of Users in this Role
            public IList<string> RolesList { get; set; }
        }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { return NotFound(); }

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            { return NotFound();}

            await Load_Form_Reference_Data(user);

            return Page();
        }


        private async Task<bool> Load_Form_Reference_Data(ApplicationUser user)
        {
            Input = new InputModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName

            };

            Input.RolesList = await UserManager.GetRolesAsync(user);

            return true;
        }



    }
}