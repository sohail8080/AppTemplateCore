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
using Microsoft.AspNetCore.Authorization;

namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{    
    public class DetailsModel : RolePageModel
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

        public InputModel Input { get; set; }


        public class InputModel : ApplicationRole
        {            
            public IList<ApplicationUser> SelectedUserList { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id);
                return NotFound();
            }
            
            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id);
                return NotFound();
            }

            Input = new InputModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            Input.SelectedUserList = new List<ApplicationUser>();

       
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
