using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class LovDA
    {
        [DisplayName("BookNames")]
        [Required(ErrorMessage = "Date is required")]
        public List<string> BookNames { get; set; }





        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$"), Required, StringLength(30)]
        public string Genre { get; set; }

        //<div class="row">  
        //        <div class="col-md-6">  
        //            <div class="form-group">  
        //                @Html.LabelFor(m => m.Gender)  
        //                @Html.DropDownList("Gender", new List<SelectListItem>()  
        //        {  
        //            new SelectListItem {Text = "Male", Value = "1"},  
        //            new SelectListItem {Text = "Female", Value = "2"}  
        //        }, "Choose Gender", new { @class = "form-control" })  
        //                @Html.ValidationMessageFor(m => m.Gender)  
        //            </div>  
        //        </div>  


    }
}
