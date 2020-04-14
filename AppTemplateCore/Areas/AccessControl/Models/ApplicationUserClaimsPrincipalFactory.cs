using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.AccessControl.Models
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {

        public ApplicationUserClaimsPrincipalFactory(
       UserManager<ApplicationUser> userManager,
       IOptions<IdentityOptions> optionsAccessor)
       : base(userManager, optionsAccessor)
        {            
        }

        
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("FirstName", user.FirstName ?? string.Empty));
            identity.AddClaim(new Claim("LastName", user.LastName ?? string.Empty));


            // When this factory is not hooked to the RPP, the User Roles
            // are automatically loaded in the ClaimsPrinciple, 
            // When this factor injected, now we need to explicityly add the 
            // the Roles info the ClaimsList of ClaimPrinciple
            var userRoles = await UserManager.GetRolesAsync(user);
            foreach(var role in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
         
            return identity;
        }


    }
}
