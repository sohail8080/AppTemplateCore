using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class StringLongDA
    {
        [DataType(DataType.MultilineText)]
        [StringLength(255)]
        public string Remarks { get; set; }


    }
}
