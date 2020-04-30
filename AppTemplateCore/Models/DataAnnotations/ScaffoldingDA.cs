using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class ScaffoldingDA
    {
        [ScaffoldColumn(false)]
        public int EmpId { get; set; }


        [ScaffoldColumn(false)]
        public int Id { get; set; }

        //specify fields for hiding from editor forms.

        //Specifies a field for hiding from editor forms.    
        //The value that specifies whether scaffolding is enabled.
        [ScaffoldColumn(false)]
        public int Id2 { get; set; }


    }
}
