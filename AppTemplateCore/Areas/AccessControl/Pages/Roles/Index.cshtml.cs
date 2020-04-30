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
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using AppTemplateCore.Areas.Common;

namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{
    public class IndexModel : RolePageModel
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

        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        //public IList<InputModel> Input { get; set; }

        public PaginatedList<InputModel> Input { get; set; }


        public class InputModel
        {

            [Required(AllowEmptyStrings = false)]
            public string Id { get; set; }

            [Required(AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
            [Display(Name = "Role Name")]
            public string Name { get; set; }


            public int PageSize { get; set; }

            // Get the list of Users in this Role
            public IList<ApplicationUser> UserList { get; set; }
        }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            // If Empty, give Value, if Value, give Empty
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            { pageIndex = 1; }
            else
            { searchString = currentFilter; }

            CurrentFilter = searchString;

            var roles = RoleManager.Roles;

            // Add search filter
            if (!String.IsNullOrEmpty(searchString))
            { roles = roles.Where(r => r.Name.Contains(searchString)); }

            // Add sort statement
            switch (sortOrder)
            {
                case "name_desc":
                    roles = roles.OrderByDescending(r => r.Name);
                    break;
                default:
                    roles = roles.OrderBy(r => r.Name);
                    break;
            }

            //await roles.ToListAsync();

            int pageSize = 3;

            var RolesList = await PaginatedList<ApplicationRole>.CreateAsync(
                roles.AsNoTracking(), pageIndex ?? 1, pageSize);


            //var roles = await RoleManager.Roles.OrderBy(r => r.Name).ToListAsync();
            //roles = await RoleManager.Roles.OrderBy(r => r.Name).ThenBy(r => r.Name).ToListAsync();
            //roles = await RoleManager.Roles.OrderByDescending(r => r.Name).ToListAsync();
            //roles = await RoleManager.Roles.OrderByDescending(r => r.Name).ThenByDescending(r => r.NormalizedName).ToListAsync();

            //Input = new List<InputModel>();
            var input = new List<InputModel>();

            foreach (var role in RolesList)
            {
                var roleWithUsers = new InputModel();
                roleWithUsers.Id = role.Id;
                roleWithUsers.Name = role.Name;
                roleWithUsers.UserList = await UserManager.GetUsersInRoleAsync(role.Name);
                input.Add(roleWithUsers);
            }

            Input = PaginatedList<InputModel>.CreateAsync(input, RolesList.TotalPages, pageIndex ?? 1, pageSize);
           
            //ApplicationRole = await Context.Roles.Include(u => u.Users).ToListAsync();
        }


        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            IdentityResult result = await RoleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {

                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }


            Logger.LogInformation($"Role {role.Name} is deleted successfully.");

            return RedirectToPage("./Index");

        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            IdentityResult result = await RoleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {

                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }


            Logger.LogInformation($"Role {role.Name} is deleted successfully.");

            return RedirectToPage("./Index");

        }


        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            { TempData["ErrorMessage"] = string.Format(Record_NotFound_Msg, id); return NotFound(); }

            IdentityResult result = await RoleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {

                foreach (var error in result.Errors)
                { ModelState.AddModelError("", error.Description); }
                return Page();
            }


            Logger.LogInformation($"Role {role.Name} is deleted successfully.");

            return RedirectToPage("./Index");

        }



    }
}
