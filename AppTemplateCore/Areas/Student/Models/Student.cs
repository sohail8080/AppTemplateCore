using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.Student.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length between 3 to 50 characters is required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Pls Enter Only Charaters")]        
        public string Name { get; set; }


        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password length between 8 to 255 characters is required.")]
        [DataType(DataType.Password)]        
        public string Password { get; set; }


        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Password is required.")]        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not matched.")]        
        public string ConfirmPassword { get; set; }


        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Email Address between 8 to 255 characters is required.")]
        [DataType(DataType.EmailAddress)]        
        //[RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "Invalid email")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }


        [DisplayName("Confirm Email")]
        [Required(ErrorMessage = "Email is required.")]
        [Compare("Email", ErrorMessage = "Email not matched.")]
        public string ConfirmEmail { get; set; }


        // Radio Buttons
        [DisplayName("Gender")]
        [Required(ErrorMessage = "Gender is required.")]
        public bool Gender { get; set; }


        [DisplayName("Credit Card")]
        [Required(ErrorMessage = "Credit Card number is required.")]
        //[StringLength(255, MinimumLength = 8, ErrorMessage = "Credit Card between 8 to 255 characters is required.")]
        [DataType(DataType.CreditCard)]
        public string CreditCard { get; set; }


        [DisplayName("Postal Code")]
        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Postal Code between 8 to 255 characters is required.")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }


        [DisplayName("Fees")]
        [Required(ErrorMessage = "Fees amount is required.")]
        [Range(10000, 20000, ErrorMessage = "Fee in range 10000 to 20000 is required.")]
        public decimal Fees { get; set; }


        [DisplayName("Salary")]
        [Required(ErrorMessage = "Salary is required.")]
        [Range(10000, 20000, ErrorMessage = "Salary in range 10000 to 20000 is required.")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Pls Enter Only Numbers")]
        public float Salary { get; set; }


        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [DisplayName("DOB")]
        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]               
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        public DateTime DateOfBirth { get; set; }


        [DisplayName("Picture")]
        [Required(ErrorMessage = "Picture is required.")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public Object PostedFile { get; set; }


        [DisplayName("Country")]
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Country Name between 3 to 15 characters is required.")]
        public string Country { get; set; }

        // [ReadOnly(true)]

        //[DisplayName("Image")]
        //[Required(ErrorMessage = "Image is required")]
        //public HttpPostedFileBase Image { get; set; }

        [DisplayName("Age")]
        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 120, ErrorMessage = "Age must be between 1-120 in years.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Pls Enter Only Numbers")]
        public int Age2 { get; set; }

        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered CONTACT_NUMBER format is not valid.")]
        //[RegularExpression(@"^(?([0-9]{3}))?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter valid number")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter PhoneNumber as 0123456789, 012-345-6789, (012)-345-6789.")]
        // [RegularExpression(@"^[0-9]*$", ErrorMessage = "Pls Enter Only Numbers")]
        // [RegularExpression("^[0-9]{8}$")]
        //Output format: 91-1234-567-890
        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Phone Number is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Phone Number between 8 to 255 characters is required.")]
        [Phone]       
        [DataType(DataType.PhoneNumber)]       
        [RegularExpression(@"^\(?([0-9]{2})[-. ]?([0-9]{4})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Remarks")]
        [Required(ErrorMessage = "Remarks are required.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Remarks between 5 to 255 characters is required.")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }


        [Display(Name = "Website URL")]
        [Required(ErrorMessage = "Website URl is required.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Website URL between 8 to 255 characters is required.")]
        [Url]
        public string WebSite { get; set; }


        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip Code is required.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Zip code between 8 to 255 characters is required.")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Please enter a valid ZIP Code (e.g. 12345, 12345-1234)")]
        public string ZipCode { get; set; }


        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "Hidden value is required")]
        public string HiddenField { get; set; }

    }
}
