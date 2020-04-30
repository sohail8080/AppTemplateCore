using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class AddressDA
    {
        [Required(ErrorMessage = "Employee Address is required")]
        [StringLength(300)]
        public string Addressfffff { get; set; }


        [Required(ErrorMessage = "Please enter Student Address")]
        [StringLength(50, ErrorMessage = "Only 50 character are allowed")]
        public string StudentAddress { get; set; }





        [DataType(DataType.PostalCode)]
        public string AddressLine1 { get; set; }



    }
}
