using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{
    public class CreateModel : PageModel
    {
        // Controller dependencies
        // Controller dependencies
        private readonly ApplicationDbContext Context;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ILogger<CreateModel> Logger;


        // Controller Contructor initializing Controller dependencies by DI Container
        public CreateModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<CreateModel> logger)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Logger = logger;
        }


        // ViewModel Properties
        // During OnGet() it will be blank
        // During OnPost() it will be filled by automatic model binding
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
            [Display(Name = "Role Name")]
            public string Name { get; set; }
        }


        // Just show the blank page
        // ViewModel Properties are used to render View
        public IActionResult OnGet()
        {
            return Page();
        }


        // OnPost(), ViewModel Properties filled and available
        // No need to catch then in Controller Action paramters
        public async Task<IActionResult> OnPostAsync()
        {

            // Model is VM Prperties
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IdentityResult result = await RoleManager.CreateAsync(
                new ApplicationRole() { Name = Input.Name });

            if (result.Succeeded)
            {
                //ViewBag.Message = "Record(s) created successfully.";
                return RedirectToPage("./Index");
            }
            else
            {
                //ViewBag.Message = "Error occurred while created Record(s)";
                Logger.LogError($"Error occurred while creating new Role");
                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }
        }




    }
}