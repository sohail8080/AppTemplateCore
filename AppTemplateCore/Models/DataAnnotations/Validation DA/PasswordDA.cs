using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class PasswordDA
    {

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DisplayName("ConfirmPassword")]
        [Required(ErrorMessage = "Password is required")]
        [Compare("Password", ErrorMessage = "Password is Not Matching")]
        public string ConfirmPassword { get; set; }


        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required()]
        //[MembershipPassword()]
        public string Password444 { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required()]
        //[MembershipPassword()]
        public string ConfirmPassword444 { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter password")]
        public string Password333222 { get; set; }


        [Required(ErrorMessage = "Please enter ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not matching")]
        public string ConfirmPassword2222 { get; set; }


        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Passwordvcvc { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        public string ConfirmPasswordlllgggg { get; set; }


        //[StringLength(MinimumLength = 3, MaximumLength = 16, ErrorMessage = "The password must have between 3 and 16 characters.")]
        public string Passwordqqqq { get; set; }




        //[StringLength(MinimumLength = 3,
        //                     MaximumLength = 16,
        //                     ErrorMessageResourceName = "StringLength",
        //                     ErrorMessageResourceType = typeof(ResoucesKeys))]
        public string Passwordpopopo { get; set; }



    }
}
