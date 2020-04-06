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

        // Controller dependencies
        protected ApplicationDbContext Context;
        protected UserManager<ApplicationUser> UserManager;
        protected RoleManager<ApplicationRole> RoleManager;
        protected SignInManager<ApplicationUser> SignInManager;
        protected ILogger<CreateModel> Logger;


        // Controller Contructor initializing Controller dependencies by DI Container
        public RolePageModel(
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

    }
}
