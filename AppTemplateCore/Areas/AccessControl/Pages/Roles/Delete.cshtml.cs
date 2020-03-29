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
    public class DeleteModel : PageModel
    {
        // Controller dependencies
        private readonly ApplicationDbContext Context;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ILogger<DeleteModel> Logger;

        public DeleteModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeleteModel> logger)
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
            public string Id { get; set; }

            [Required(AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
            [Display(Name = "Role Name")]
            public string Name { get; set; }

            // Get the list of Users in this Role
            public List<ApplicationUser> SelectedUserList { get; set; }
        }



        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { return NotFound(); }

            //var role = await Context.Roles.FirstOrDefaultAsync(m => m.Id == id);
            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            { return NotFound(); }

            await Load_Form_Reference_Data(role);
            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {

            if (string.IsNullOrEmpty(Input.Id))
            { return NotFound(); }

            var role = await RoleManager.FindByIdAsync(Input.Id);

            if (role == null)
            {
                //ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";                
                return NotFound();                
            }

            IdentityResult result = await RoleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                //ViewBag.Message = "Error occurred while deleting Record(s)";
                Add_Model_Errors(result);
                await Load_Form_Reference_Data(role);
                return Page();
            }

            //ViewBag.Message = "Record(s) deleted successfully.";
            Logger.LogInformation($"Role {Input.Name} is deleted successfully.");
            return RedirectToPage("./Index");

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

        private void Add_Model_Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }






    }
}
