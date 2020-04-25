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
using Microsoft.AspNetCore.Authorization;

namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{    
    public class EditModel5 : RolePageModel
    {

        public EditModel5(
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

            public IList<ApplicationUser> SelectedUserList { get; set; }

        }
        
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            await Load_Page_Reference_Data(role);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, Input.Id); return NotFound(); }

            var role = await RoleManager.FindByIdAsync(Input.Id);

            if (role == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, Input.Id); return NotFound(); }
            
            if (!ModelState.IsValid)
            {
                await Load_Page_Reference_Data(role);
                return Page();
            }

            role.Name = Input.Name;

            IdentityResult result = await RoleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                Handle_Error_Response(result);
                await Load_Page_Reference_Data(role);
                return Page();
            }

            Handle_Success_Response(result);
            return RedirectToPage("./Index");
        }

        private async Task<bool> Load_Page_Reference_Data(ApplicationRole role)
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

        private void Handle_Success_Response(IdentityResult result)
        {
            Logger.LogInformation(string.Format(Edit_Success_Msg, Input.Name));
            StatusMessage = string.Format(Edit_Success_Msg, Input.Name);
        }

        private void Handle_Error_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Edit_Failed_Msg, Input.Name));
            StatusMessage = string.Format(Edit_Failed_Msg, Input.Name);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }

        private bool ApplicationRoleExists(string id)
        {
            return Context.Roles.Any(e => e.Id == id);
        }
    }
}
