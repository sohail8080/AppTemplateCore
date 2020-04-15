using AppTemplateCore.Areas.AccessControl.Models;
using AppTemplateCore.Data;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = RolesStore.Admin)]
    public class UserPageModel : PageModel
    {

        internal ApplicationDbContext Context;
        internal UserManager<ApplicationUser> UserManager;
        internal RoleManager<ApplicationRole> RoleManager;
        internal SignInManager<ApplicationUser> SignInManager;
        internal ILogger Logger;

        [TempData]
        public string StatusMessage { get; set; }
        internal readonly string Create_All_Msg = "Successfully created all Users.";
        internal readonly string Create_Success_Msg = "Successfully created User : \"{0}\"";
        internal readonly string Create_Failed_Msg = "Error occurred while creating new User : \"{0}\"";

        internal readonly string Edit_All_Msg = "Successfully modified all Users.";
        internal readonly string Edit_Success_Msg = "Successfully modified User : \"{0}\"";
        internal readonly string Edit_Failed_Msg = "Error occurred while modifying User : \"{0}\"";

        internal readonly string Delete_All_Msg = "Successfully deleted all Users.";
        internal readonly string Delete_Success_Msg = "Successfully deleted User : \"{0}\"";
        internal readonly string Delete_Failed_Msg = "Error occurred while deleting User : \"{0}\"";

        internal readonly string Record_NotFound_Msg = "User with Id \"{0}\" cannot be found";


    }
}
