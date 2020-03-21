using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppTemplateCore.Areas.Identity.Pages
{
    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {

        public string RequestId { get; set; }


        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


        public void OnGet()
        {
            //     Encapsulates all HTTP-specific information about an individual HTTP request.
            //     Gets or sets a unique identifier to represent this request in trace logs.
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
