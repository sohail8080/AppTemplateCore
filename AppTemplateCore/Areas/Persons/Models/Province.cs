using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.Persons.Models
{
    public enum Province
    {
        Punjab,
        Sindh,
        KPK,
        Balochistan,
        Kashmir,
        GilgitBaltistan
    }

    //You can mark your enumerator list with the Display attribute 
    //to get a richer UI: spaces are now allowed other wise
    public enum CountryEnum
    {
        [Display(Name = "United Mexican States")]
        Mexico,
        [Display(Name = "United States of America")]
        USA,
        Canada,
        France,
        Germany,
        Spain
    }


    //<select data-val="true" data-val-required="The EnumCountry field is required."
    //             id="EnumCountry" name="EnumCountry">
    //         <option value = "0" > United Mexican States</option>
    //             <option value = "1" > United States of America</option>
    //             <option value = "2" > Canada </ option >

    //             < option value= "3" > France </ option >

    //             < option value= "4" > Germany </ option >

    //             < option selected= "selected" value= "5" > Spain </ option >

    //         </ select >




    public class CountryViewModelGroup
    {
        public CountryViewModelGroup()
        {
            var NorthAmericaGroup = new SelectListGroup { Name = "North America" };
            var EuropeGroup = new SelectListGroup { Name = "Europe" };

            Countries = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = "MEX",
                Text = "Mexico",
                Group = NorthAmericaGroup
            },
            new SelectListItem
            {
                Value = "CAN",
                Text = "Canada",
                Group = NorthAmericaGroup
            },
            new SelectListItem
            {
                Value = "US",
                Text = "USA",
                Group = NorthAmericaGroup
            },
            new SelectListItem
            {
                Value = "FR",
                Text = "France",
                Group = EuropeGroup
            },
            new SelectListItem
            {
                Value = "ES",
                Text = "Spain",
                Group = EuropeGroup
            },
            new SelectListItem
            {
                Value = "DE",
                Text = "Germany",
                Group = EuropeGroup
            }
      };
        }

        public string Country { get; set; }

        public List<SelectListItem> Countries { get; }

    }



    }
