using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Areas.AccessControl.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Required]
        [StringLength(15)]
        public string FirstName { get; set; }

        // Personal Data Atrributs are included while 
        // Profile Data is Downloaded.
        [PersonalData]
        [Required]
        [StringLength(15)]
        public string LastName { get; set; }

    }
}
