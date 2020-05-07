using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class GeneralDA
    {
        //It set a property to read-only.

        [ReadOnly(true)]
        public string Name { get; private set; }

        [Compare("Email", ErrorMessage = "Email Not Matched")]
        public string ConfirmEmail { get; set; }


    }
}
