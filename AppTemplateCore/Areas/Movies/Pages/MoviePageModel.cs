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

namespace AppTemplateCore.Areas.AccessControl.Pages.Movies
{
    [Authorize(Roles = RolesStore.Admin)]
    public class MoviePageModel : PageModel
    {

        internal ApplicationDbContext Context;
        internal UserManager<ApplicationUser> UserManager;
        internal RoleManager<ApplicationRole> RoleManager;
        internal SignInManager<ApplicationUser> SignInManager;
        internal ILogger Logger;


        [TempData]
        public string StatusMessage { get; set; }
        internal readonly string Create_All_Msg = "Successfully created all Movies.";
        internal readonly string Create_Success_Msg = "Successfully created Movie : \"{0}\"";
        internal readonly string Create_Failed_Msg = "Error occurred while creating new Movie : \"{0}\"";

        internal readonly string Edit_All_Msg = "Successfully modified all Movie.";
        internal readonly string Edit_Success_Msg = "Successfully modified Movie : \"{0}\"";
        internal readonly string Edit_Failed_Msg = "Error occurred while modifying Movie : \"{0}\"";

        internal readonly string Delete_All_Msg = "Successfully deleted all Movies.";
        internal readonly string Delete_Success_Msg = "Successfully deleted Movie : \"{0}\"";
        internal readonly string Delete_Failed_Msg = "Error occurred while deleting Movie : \"{0}\"";

        internal readonly string Record_NotFound_Msg = "Movie with Id \"{0}\" cannot be found";


    }
}
