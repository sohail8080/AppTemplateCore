using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class GenderDA
    {

        [Required(ErrorMessage = "Please choose gender")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Choose your gender")]
        public string Genderttt { get; set; }


        //<div class="col-md-6">  
        //           <div class="form-group">  
        //               <label asp-for="Gender" class="lable-control"></label>  
        //               <select class="custom-select">  
        //                   <option value = "" > Choose Gender</option>  
        //                   <option value = "Male" > Male </ option >
        //                   < option value="Female">Female</option>  
        //               </select>  
        //               <span asp-validation-for="Gender" class="text-danger"></span>  
        //           </div>  
        //       </div>  


    }
}
