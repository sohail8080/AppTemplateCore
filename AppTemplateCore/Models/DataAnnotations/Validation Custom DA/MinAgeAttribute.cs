using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.CustomDA
{
    public class MinAgeAttribute : ValidationAttribute
    {
        //[Required(ErrorMessage = "Age is Required")]
        //[MinAge(18)]
        //public int Age { get; set; }


        private int _minAge;

        public MinAgeAttribute(int value)
        {
            _minAge = value;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is int)
                {
                    int minimumage = (int)value;
                    if (minimumage < _minAge)
                    {
                        return new ValidationResult("Minimum age must be " + _minAge);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
