using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.Persons.Models
{
    public class Hobby
    {
        public int HobbyID { get; set; }

        [Required]
        public string Name { get; set; }

        public List<PersonHobby> PersonHobbies { get; set; }

    }
}
