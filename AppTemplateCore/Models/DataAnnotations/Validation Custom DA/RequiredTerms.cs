using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.CustomDA
{
    //[RequiredTerms(ErrorMessage = "Must accept our terms")]
    //public Boolean terms { get; set; }


    public sealed class RequiredTerms : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToBoolean(value) == false)
                return new ValidationResult(ErrorMessage);
            else
                return ValidationResult.Success;
        }
    }
}
