using Microsoft.AspNetCore.Identity;
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

    public class ApplicationEmailConfirmationTokenProviderOptions 
        : DataProtectionTokenProviderOptions
    {
    }
}
