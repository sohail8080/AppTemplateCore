using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.CustomDA
{
    public class SkillValidation : ValidationAttribute
    {

        //[SkillValidation(ErrorMessage = "Select at least 3 skills")]
        //public List<CheckBox> skills { get; set; }


        //<td>
        //@Html.LabelFor(model => model.skills)
        //@Html.CheckBoxFor(m => m.skills[0].IsChecked, new { id = "csharpSkill" }) C#
        //@Html.CheckBoxFor(m => m.skills[1].IsChecked, new { id = "aspSkill" }) ASP.NET
        //@Html.CheckBoxFor(m => m.skills[2].IsChecked, new { id = "jquerySkill" }) jQuery
        //@Html.CheckBoxFor(m => m.skills[3].IsChecked, new { id = "mvcSkill" }) ASP.NET MVC
        //@Html.CheckBoxFor(m => m.skills[4].IsChecked, new { id = "razorSkill" }) Razor
        //@Html.CheckBoxFor(m => m.skills[5].IsChecked, new { id = "htmlSkill" }) HTML
        //@Html.ValidationMessageFor(model => model.skills)
        //</td>



        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<CheckBox> instance = value as List<CheckBox>;
            int count = instance == null ? 0 : (from p in instance
                                                where p.IsChecked == true
                                                select p).Count();
            if (count >= 3)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    }


    public class CheckBox
    {
        public bool IsChecked { get; set; }
    }

}
