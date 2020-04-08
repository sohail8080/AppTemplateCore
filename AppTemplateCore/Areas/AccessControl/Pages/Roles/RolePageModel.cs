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

namespace AppTemplateCore.Areas.AccessControl.Pages.Roles
{
    public class RolePageModel : PageModel
    {
        
        internal ApplicationDbContext Context;
        internal UserManager<ApplicationUser> UserManager;
        internal RoleManager<ApplicationRole> RoleManager;
        internal SignInManager<ApplicationUser> SignInManager;
        internal ILogger Logger;

        [TempData]
        public string StatusMessage { get; set; }

        internal readonly string Create_All_Msg = "Successfully created all Roles.";
        internal readonly string Create_Success_Msg = "Successfully created Role : \"{0}\"";
        internal readonly string Create_Failed_Msg = "Error occurred while creating new Role : \"{0}\"";

        internal readonly string Edit_All_Msg = "Successfully modified all Roles.";
        internal readonly string Edit_Success_Msg = "Successfully modified Role : \"{0}\"";
        internal readonly string Edit_Failed_Msg = "Error occurred while modifying Role : \"{0}\"";

        internal readonly string Delete_All_Success_Msg = "Successfully deleted all Roles.";
        internal readonly string Delete_All_Failed_Msg = "Error occurred while deleting all Roles.";
        internal readonly string Delete_Success_Msg = "Successfully deleted Role : \"{0}\"";
        internal readonly string Delete_Failed_Msg = "Error occurred while deleting Role : \"{0}\"";

        internal readonly string Record_NotFound_Msg = "Role with Id \"{0}\" cannot be found";

    }
}
