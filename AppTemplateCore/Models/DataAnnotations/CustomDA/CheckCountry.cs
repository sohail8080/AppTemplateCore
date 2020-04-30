using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.CustomDA
{
    public sealed class CheckCountry : ValidationAttribute
    {
        public String AllowCountry { get; set; }
        protected override ValidationResult IsValid(object country, ValidationContext validationContext)
        {
            string[] myarr = AllowCountry.ToString().Split(',');
            if (myarr.Contains(country))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Please choose a valid country eg.(India,Pakistan,Nepal");
            }
        }
    }
}
