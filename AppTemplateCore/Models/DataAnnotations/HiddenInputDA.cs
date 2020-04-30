using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class HiddenInputDA
    {
        [HiddenInput(DisplayValue = false)]
        public string Name { get; set; }

    }
}
