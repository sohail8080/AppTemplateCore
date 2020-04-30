using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class CreditCardDA
    {
        //specifies that a data field value is credit card number.

        [DataType(DataType.CreditCard)]
        public string Name { get; set; }


    }
}
