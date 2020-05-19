using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.Persons.Models
{
    public class Country
    {
        public int CountryID { get; set; }

        [Required]
        public string Name { get; set; }

        public int Population { get; set; }

    }
}
