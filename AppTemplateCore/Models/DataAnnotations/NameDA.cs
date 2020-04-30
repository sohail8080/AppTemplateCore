using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class NameDA
    {




        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Pls Enter Only Charaters")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name Length Minimun 3 is required")]
        public string Ename { get; set; }


        [Required(ErrorMessage = "Name is Required")]
        [DisplayName("Enter Your Name: ")]
        [StringLength(50, MinimumLength = 3)]
        [MaxLength(50)]
        public string Name { get; set; }


        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "Employee Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public String EmpName { get; set; }


        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 3)]
        public string Name2 { get; set; }


        [Required(ErrorMessage = "Required")]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Required")]
        [StringLength(100)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }


        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        [Required()]
        public string UserName { get; set; }


        [RegularExpression(@"^[a-z]{8,16}?$", ErrorMessage = "A User Name must consist of 8-16 lowercase letters")]
        public string UserName000 { get; set; }


        // set the maximum
        [StringLength(20, ErrorMessage = "The username cannot exceed 20 characters. ")]
        public string Usernameaaa { get; set; }

        //[StringLength(20, ErrorMessageResourceName = "StringLength",
        //                      ErrorMessageResourceType = typeof(ResoucesKeys))]
        public string Username { get; set; }

        [DisplayName("StudentName")]
        [Required(ErrorMessage = "Enter Student Name")]
        [StringLength(50, ErrorMessage = "Only 50 character are allowed")]
        public string StudentName { get; set; }



        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [Display(Name = "Student Name")]
        public string NameFDFD { get; set; }



        [DisplayName("First Name")]
        [Required(ErrorMessage = "The First Name is required.")]
        [MinLength(5, ErrorMessage = "The First Name must be atleast 5 characters")]
        [MaxLength(15, ErrorMessage = "The First Name cannot be more than 15 characters")]
        public string FirstNameBBBBB { get; set; }

        [DisplayName("Last Name")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "The Last Name should be between 5 to 15 characters")]
        public string LastNameBBBBB { get; set; }


        [DisplayName("Delicious Treat")]
        [Required(ErrorMessage = "The product name field is required.")]
        public string Name1111 { get; set; }



    }
}
