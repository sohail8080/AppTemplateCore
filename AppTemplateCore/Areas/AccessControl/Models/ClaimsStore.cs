using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.AccessControl.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
    {
        new Claim(type:"Create Role", value:"Create Role"),
        new Claim(type:"Edit Role",value:"Edit Role"),
        new Claim(type:"Delete Role",value:"Delete Role")
    };

    }
}
