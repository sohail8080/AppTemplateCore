using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppTemplateCore.Areas.Identity.Pages.Account.Manage
{
    // This Class is just to fix the CSS class of Menu Links
    // Highlight/Standout the Current Page Link with Active Class
    // No special CSS class for the other Page Links

    public static class ManageNavPages
    {
        // static property, get only property
        public static string Index => "Index";

        public static string ChangePassword => "ChangePassword";

        public static string ExternalLogins => "ExternalLogins";

        public static string PersonalData => "PersonalData";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        // static method having only one statement
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        private static string PageNavClass(ViewContext viewContext, string page)
        {

            // This method is just to fix the CSS class of Menu Links
            // Highlight/Standout the Current Page Link with Active Class
            // No special CSS class for the other Page Links

            // If ActivePage from the ViewDataDictionary is NULL then Show

            //     Gets ActionDescriptor for the selected action.

            //     Compare strings using ordinal (binary) sort rules and ignoring the case of the
            //     strings being compared.

            // Get the Active Page form ViewContext if it is there. if not then
            // Get this from the IO.Path
            //


            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}