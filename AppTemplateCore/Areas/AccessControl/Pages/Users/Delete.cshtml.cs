using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class DeleteModel : UserPageModel
    {

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
            { return NotFound(); }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Id))
            { return NotFound(); }

            var user = await UserManager.FindByIdAsync(Input.Id);

            if (user == null)
            { return NotFound(); }

            IdentityResult result = await UserManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                Handle_Error_Response(result);
                await Load_Form_Reference_Data(user);
                return Page();
            }

            Handle_Success_Response(result);

            return RedirectToPage("./Index");
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


        private void Handle_Success_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Delete_Success_Msg, Input.Email));
            StatusMessage = string.Format(Delete_Success_Msg, Input.Email);
        }

        private void Handle_Error_Response(IdentityResult result)
        {
            Logger.LogError(string.Format(Delete_Failed_Msg, Input.Email));
            StatusMessage = string.Format(Delete_Failed_Msg, Input.Email);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }


    }
}