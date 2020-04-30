using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.StartupExtensions
{
    public class External_Authentication_Options
    {
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }

        public string FacebookAppId { get; set; }
        public string FacebookAppSecret { get; set; }

    }
}
