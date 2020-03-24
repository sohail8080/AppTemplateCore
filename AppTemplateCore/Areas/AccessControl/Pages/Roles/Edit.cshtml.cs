using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{
    public class EditModel : PageModel
    {
        // Controller dependencies : UOW
        // Controller dependencies
        private readonly ApplicationDbContext Context;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ILogger<EditModel> Logger;

        // DI Container injects UOW inside controller constructor
        public EditModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<EditModel> logger)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Logger = logger;
        }

        // View Model Properties available in View
        // OnGet() we fill this property and show Page();
        // OnPost() we do not get this property as ActionParameters
        // When form is posted this property is Auto Filled and availble
        // ModelState works as before in Controller
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
            public List<ApplicationUser> UserList { get; set; }

        }

        // OnGet(), fill ViewModel Propertis and show Page();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {return NotFound();}

            // ViewModel Property filling
            //var role = await Context.Roles.FirstOrDefaultAsync(m => m.Id == id);
            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {return NotFound();}


            Input = new InputModel()
            {
                Id = role.Id,
                Name = role.Name
            };


            Input.UserList = UserManager.Users.ToList();

            // Get the list of Users in this Role
            //foreach (var user in UserManager.Users.ToList())
            //{
            //    if (await UserManager.IsInRoleAsync(user, role.Name))
            //    {
            //        Input.UserList.Add(user);
            //    }
            //}


            // Show Page
            return Page();
        }


        // OnPost(), ViewModel Properties filled and available
        // No need to catch then in Controller Action paramters
        public async Task<IActionResult> OnPostAsync()
        {
            // Here Model means all Properties of this class
            if (!ModelState.IsValid)
            {
                // VM Properties already filled, show page again
                return Page();
            }

            //var roleInDB = _context.Roles.FirstOrDefault(r => r.Id == viewModel.Id);
            var role = await RoleManager.FindByIdAsync(Input.Id);

            if (role == null)
            {
                ModelState.AddModelError("", $"Role {Input.Name} cannot be found");
                return Page();
                //return HttpNotFound();
                //return NotFound();
            }

            role.Name = Input.Name;

            IdentityResult result = await RoleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                //ViewBag.Message = "Record(s) updated successfully.");
                Logger.LogInformation($"Role {Input.Name} is updated successfully.");
                // Show List Page
                return RedirectToPage("./Index");
            }
            else
            {
                //ViewBag.Message = "Error occurred while updating Record(s)";
                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }
        }


        private bool ApplicationRoleExists(string id)
        {
            return Context.Roles.Any(e => e.Id == id);
        }
    }
}
