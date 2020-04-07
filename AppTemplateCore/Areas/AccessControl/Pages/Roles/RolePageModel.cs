﻿using AppTemplateCore.Areas.AccessControl.Models;
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
        internal readonly string Success_Msg = "Successfully deleted Role : {0}";
        internal readonly string Error_Msg = "Error occurred while deleting new Role : {0}";
        internal readonly string NotFound_Msg = "Role with Id \"{0}\" cannot be found";

    }
}
