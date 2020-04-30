using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class BooleanDA
    {
        [DisplayName("Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public bool Gender { get; set; }


    //     <div class="form-group">
    //    @Html.LabelFor(model => model.Gender, htmlAttributes: new { @class = "control-label col-md-2" })
    //    <div class="col-md-10">
    //        <div class="checkbox">
    //            @* @Html.RadioButtonFor(m => m.Gender, "true") <label> Male</label>
    //               @Html.RadioButtonFor(m => m.Gender, "false") <label> Female</label>*@

    //            @Html.DropDownListFor(model => model.Gender, new SelectList(new List<Object>{
    //                                 new { value = "" , text = "Select"  },
    //                                 new { value = 1 , text = "Male" },
    //                                 new { value = 0 , text = "Female"} }, "value", "text", 0))


    //            @Html.ValidationMessageFor(model => model.Gender, "", new { @class = "text-danger" })
    //        </div>
    //    </div>
    //</div>




    }
}
