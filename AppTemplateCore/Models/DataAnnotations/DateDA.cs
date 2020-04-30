using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class DateDA
    {

        //Specifies a display format for a property like Date Format, 
        // Currency format etc.
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        public System.DateTime? HireDate { get; set; }


        [DisplayName("Date")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime DateandTime { get; set; }


        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Required(ErrorMessage = "Please enter Date of Birth")]
        public DateTime StudentDOB { get; set; }


        [DisplayName("Freshly Baked on")]
        [Required(ErrorMessage = "The Freshly Baked On field is required.")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Don't Sell After")]
        [Required(ErrorMessage = "The Expiration Date field is required.")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Choose date of birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        //     <div class="form-group">
        //    @Html.LabelFor(model => model.DateandTime, htmlAttributes: new { @class = "control-label col-md-2" })
        //    <div class="col-md-10">
        //        @Html.EditorFor(model => model.DateandTime, new { htmlAttributes = new { @class = "form-control", @type = "date" } })
        //        @Html.ValidationMessageFor(model => model.DateandTime, "", new { @class = "text-danger" })

        //    </div>
        //</div>

        [Display(Name = "Release Date"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }

        [Required(ErrorMessage = "Choose batch time")]
        [Display(Name = "Batch Time")]
        [DataType(DataType.Time)]
        public DateTime BatchTime { get; set; }



    }
}
