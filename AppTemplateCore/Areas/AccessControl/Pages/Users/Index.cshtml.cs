using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Areas.Common;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppTemplateCore.Areas.AccessControl.Pages.Users
{
    public class IndexModel : UserPageModel
    {
        public IndexModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<IndexModel> logger)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Logger = logger;
        }


        public string UserNameSort { get; set; }
        public string FirstNameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<InputModel> Input { get; set; }

        // public IList<InputModel> Input { get; set; }


        public class InputModel : ApplicationUser
        {

            public IList<string> RolesList { get; set; }
        }


        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            UserNameSort = String.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            FirstNameSort = sortOrder == "FirstName" ? "firstname_desc" : "FirstName";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            var users = UserManager.Users;//.ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.LastName.Contains(searchString)
                                       || u.FirstName.Contains(searchString)
                                       || u.Email.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "username_desc":
                    users = users.OrderByDescending(u => u.UserName);
                    break;
                case "FirstName":
                    users = users.OrderBy(u => u.FirstName);
                    break;
                case "firstname_desc":
                    users = users.OrderByDescending(u => u.FirstName);
                    break;
                default:
                    users = users.OrderBy(u => u.UserName);
                    break;
            }


            //await users.ToListAsync();

            int pageSize = 3;

            var UsersList = await PaginatedList<ApplicationUser>.CreateAsync(
                users.AsNoTracking(), pageIndex ?? 1, pageSize);


            //Input = new List<InputModel>();
            var input = new List<InputModel>();

            foreach (var user in UsersList)
            {
                var userWithRoles = new InputModel();
                userWithRoles.Id = user.Id;
                userWithRoles.UserName = user.UserName;
                userWithRoles.Email = user.Email;

                userWithRoles.FirstName = user.FirstName;
                userWithRoles.LastName = user.LastName;
                userWithRoles.PhoneNumber = user.PhoneNumber;

                userWithRoles.RolesList = await UserManager.GetRolesAsync(user);

                input.Add(userWithRoles);
            }


            Input = PaginatedList<InputModel>.CreateAsync(input, UsersList.TotalPages, pageIndex ?? 1, pageSize);

        }


      
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            IdentityResult result = await UserManager.DeleteAsync(user);

            if (!result.Succeeded)
            {

                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }


            Logger.LogInformation($"Role {user.UserName} is deleted successfully.");

            return RedirectToPage("./Index");

        }


        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }


            IdentityResult result = await UserManager.DeleteAsync(user);

            if (!result.Succeeded)
            {

                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }


            Logger.LogInformation($"Role {user.UserName} is deleted successfully.");

            return RedirectToPage("./Index");

        }


        private void Handle_Success_Response(IdentityResult result, ApplicationUser user)
        {
            Logger.LogInformation(string.Format(Delete_Success_Msg, user.Email));
            StatusMessage = string.Format(Delete_Success_Msg, user.Email);
        }


        private void Handle_Error_Response(IdentityResult result, ApplicationUser user)
        {
            Logger.LogError(string.Format(Delete_Failed_Msg, user.Email));
            StatusMessage = string.Format(Delete_Failed_Msg, user.Email);
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }

    }
}