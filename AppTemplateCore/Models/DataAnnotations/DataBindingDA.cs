using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    //[Bind(Exclude = "Id")]
    public class DataBindingDA
    {

        //specify fields to include or exclude when adding parameter 
        //or form values to model properties.

        public string Id { get; set; }
        //Include or Exclude value when adding form values to model properties.




    }
}
