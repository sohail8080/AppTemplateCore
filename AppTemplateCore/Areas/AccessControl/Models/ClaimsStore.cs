using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.AccessControl.Models
{
    public static class ClaimsStore
    {
        public const string Create_Role = "CreateRole";
        public const string Edit_Role = "EditRole";
        public const string Delete_Role = "DeleteRole";

        public static List<Claim> AllClaims = new List<Claim>()
    {
            // as we are using Claims to represent Roles, those are represented by 
            // boolean value, means if Claim is there for User in UserClaims table, it is true. 
            // absence of record in UserClaims table means, No Claim is assigned.
        new Claim(type:Create_Role, value:"true"),
        new Claim(type:Edit_Role, value:"true"),
        new Claim(type:Delete_Role,value:"true")
    };

        // This claim value is given by User that is only Claim Name is given in string
        public const string Department = "Department";


    }
}
