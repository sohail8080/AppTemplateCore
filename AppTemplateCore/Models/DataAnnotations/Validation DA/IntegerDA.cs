using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class IntegerDA
    {
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        //[Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]

        // for numbers that need to start with a zero
        //[RegularExpression("([0-9]+)")]


        // for numbers that begin from 1
        //[RegularExpression("([1-9][0-9]*)")]


        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public int MaxJsonLength { get; set; }


        [Range(1, 120, ErrorMessage = "Age must be between 1-120 in years.")]
        public int Age { get; set; }


        [Required(ErrorMessage = "Age is Required")]
        [Range(1, 120, ErrorMessage = "Age must be between 1-120 in years.")]
        public int Age2 { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(3000, 10000000, ErrorMessage = "Salary must be between 3000 and 10000000")]
        public int Salary { get; set; }


        [DisplayName("Age")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Pls Enter Only Numbers")]
        [Required(ErrorMessage = "Age is required")]
        public int Age222 { set; get; }

        [DisplayName("Qty Available")]
        [Required(ErrorMessage = "The Qty Available field is required.")]
        [Range(0, 120)]
        public int QtyOnHand { get; set; }


        [DisplayName("Qty Available")]
        [Required(ErrorMessage = "The Qty Available field is required.")]
        [Range(0, 120)]
        public int QtyOnHandEEE { get; set; }

        [Range(0, 120, ErrorMessage = "Please enter between 0 to 120")]
        public string Agerrrr { get; set; }

    }
}
