using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class CurrencyDA
    {
        [Range(5000, 15000, ErrorMessage = "Please enter valid range")]
        [Required(ErrorMessage = "Please enter Student Fees")]
        public decimal StudentFees { get; set; }


        [DisplayName("Salary")]
        [Required(ErrorMessage = "The Salary is required.")]
        [Range(10000, 20000)]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public float Salary { get; set; }


        [DisplayName("Sale Price")]
        [Required(ErrorMessage = "The Sale Price field is required.")]
        public decimal Price { get; set; }

        [RegularExpression(@"^(0(?!\.00)|[1-9]\d{0,6})\.\d{2}$", ErrorMessage = "Invalid salary 3500.50")]
        public decimal expectedMonthlySalary { get; set; }

        [Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Pricepop { get; set; }



        [DisplayName("Employee salary")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Pls Enter Only Numbers")]
        [Required(ErrorMessage = "salary is required")]
        [Range(3000, 10000000, ErrorMessage = "Salary must be between 3000 and 10000000")]
        public int salary2 { get; set; }


    }
}
