using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class URL
    {

        //specifies URL validation.

        [Url]
        [Required]
        public string URLaa { get; set; }

        [Required(ErrorMessage = "Please enter website url")]
        [Display(Name = "Website Url")]
        [Url]
        public string WebSite { get; set; }


    }
}
