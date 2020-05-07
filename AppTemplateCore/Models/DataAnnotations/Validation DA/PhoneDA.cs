using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class PhoneDA
    {

        //specifies that an input field value is well-formed phone number 
        // using Regular Expression.

        //Output format: 91-1234-567-890

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})[-. ]?([0-9]{4})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid Phone number")]
        public string Name { get; set; }



        [DisplayName("PHONENumber")]
        [Required(ErrorMessage = "PhoneNmber is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Pls Enter Only Numbers")]
        [StringLength(10, ErrorMessage = "maxlimit 10")]
        public string PhoneNmber { get; set; }


        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]{8}$")]
        [StringLength(32)]
        public string Phone { get; set; }



        [Required(ErrorMessage = "Please enter Mobile No")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public int Mobile { get; set; }

        
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email ID")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter PhoneNumber as 0123456789, 012-345-6789, (012)-345-6789.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Please enter  phone number")]
        [RegularExpression(@"^(?([0-9]{3}))?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter valid number")]
        public string Phone444333 { get; set; }

        [Required(ErrorMessage = "Enter phone number")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumberkkkk { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumbernnnnvvv { get; set; }

        //[AcceptVerbs("GET", "POST")]
        //public IActionResult VerifyPhone(
        //    [RegularExpression(@"^\d{3}-\d{3}-\d{4}$")] string phone)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Json($"Phone {phone} has an invalid format. Format: ###-###-####");
        //    }

        //    return Json(true);
        //}


    }


}
