using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.AccessControl.Pages.Users
{
    public class UserPageModel : PageModel
    {

        internal ApplicationDbContext Context;
        internal UserManager<ApplicationUser> UserManager;
        internal RoleManager<ApplicationRole> RoleManager;
        internal SignInManager<ApplicationUser> SignInManager;
        internal ILogger Logger;

        [TempData]
        public string StatusMessage { get; set; }
        internal readonly string Success_Msg = "Successfully deleted User : {0}";
        internal readonly string Error_Msg = "Error occurred while deleting new User : {0}";
        internal readonly string NotFound_Msg = "User with Id \"{0}\" cannot be found";

    }
}
