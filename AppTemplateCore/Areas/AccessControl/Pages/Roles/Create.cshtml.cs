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
using Microsoft.EntityFrameworkCore;



namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{
    public class CreateModel : PageModel
    {
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

        [TempData]
        public string StatusMessage { get; set; }
        private readonly string Success_Msg = "Successfully created new Role : {0}";
        private readonly string Error_Msg = "Error occurred while creating new Role : {0}";

        // ViewModel Properties
        // During OnGet() it will be blank
        // During OnPost() it will be filled by automatic model binding
        [BindProperty]
        public InputModel Input { get; set; }

        // Fit to purpose ViewModel should be used in case of Add/Edit
        public class InputModel
        {
            [Required(AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
            [Display(Name = "Role Name")]
            public string Name { get; set; }

            // Get the list of Users in this Role
            public List<RoleHasUsers> AllUsersList { get; set; }

        }


        public class RoleHasUsers
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public bool IsSelected { get; set; }

        }

        // Just show the blank page
        // ViewModel Properties are used to render View
        public async Task<IActionResult> OnGetAsync()
        {
            await Load_Form_Reference_Data();
            return Page();
        }


        // OnPost(), ViewModel Properties filled and available
        // No need to catch then in Controller Action paramters
        public async Task<IActionResult> OnPostAsync()
        {

            // Model is VM Prperties
            if (!ModelState.IsValid)
            {
                await Load_Form_Reference_Data_OnPost_Failed();
                return Page();
            }

            IdentityResult result = await RoleManager.CreateAsync(
                new ApplicationRole() { Name = Input.Name });

            if (!result.Succeeded)
            {
                await Handle_Error_Response(result);
                return Page();
            }

            Handle_Success_Response(result);
            return RedirectToPage("./Index");

        }


        private async Task<bool> Load_Form_Reference_Data()
        {
            Input = new InputModel();

            var All_Users = await UserManager.Users.ToListAsync();

            Input.AllUsersList = All_Users.Select(user => new RoleHasUsers()
            {
                IsSelected = false,
                UserId = user.Id,
                UserName = user.UserName
            }).ToList();

            return true;
        }

        private async Task<bool> Load_Form_Reference_Data_OnPost_Failed()
        {            
            var All_Users = await UserManager.Users.ToListAsync();

            Input.AllUsersList = All_Users.Select(user => new RoleHasUsers()
            {
                IsSelected = false,
                UserId = user.Id,
                UserName = user.UserName
            }).ToList();

            return true;
        }

        private void Handle_Success_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Success_Msg, Input.Name));
            StatusMessage = string.Format(Success_Msg, Input.Name);
        }

        private async Task Handle_Error_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Error_Msg, Input.Name));
            StatusMessage = string.Format(Error_Msg, Input.Name);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
            await Load_Form_Reference_Data_OnPost_Failed();
        }

    }
}