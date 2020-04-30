using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public class IsVipRequirement : IAuthorizationRequirement
    {

        public IsVipRequirement(string airline)
        {
            Airline = airline;
        }

        public string Airline { get; }

    }
}
