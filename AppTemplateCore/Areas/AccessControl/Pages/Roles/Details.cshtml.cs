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


        public class InputModel
        {

            [Required(AllowEmptyStrings = false)]
            public string Id { get; set; }

            [Required(AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
            [Display(Name = "Name")]
            public string Name { get; set; }

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

            await Load_Form_Reference_Data(role);
           
            return Page();
        }




        private async Task<bool> Load_Form_Reference_Data(ApplicationRole role)
        {

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

            return true;
        }




    }
}
