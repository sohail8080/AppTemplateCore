using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    // Following class is introduced to set the life span of only 
    // Email confirmation token.
    // We need to have this class if we want customize the EmailConfirmationToken 
    // seperately

    public class ApplicationEmailConfirmationTokenProvider<TUser>
        : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public ApplicationEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
                                        IOptions<ApplicationEmailConfirmationTokenProviderOptions> options)
            : base(dataProtectionProvider, options)
        { }
    }
}
