using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppTemplateCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailInfoAfterLoginModel : PageModel
    {

        [TempData]
        public string EmailConfirmationToken { get; set; }


        public void OnGet()
        {
        }
    }
}