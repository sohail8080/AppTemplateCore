using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    //Data Annotations - Required Attribute in EF 6 & EF Core
    //The Required attribute can be applied to one or more properties in an entity class. 
    //EF will create a NOT NULL column in a database table for a property on which the 
    //Required attribute is applied.

    using System.ComponentModel.DataAnnotations;

    public class Studentcccddd
    {
        public int StudentID { get; set; }

        [Required]
        public string StudentName { get; set; }
    }


    //Data Annotations Required Attribute EF 6 & EF Core

    //The Required attribute is used for generating NOT NULL columns.
    //EF 6 & EF Core will create a NOT NULL column if it detects a property having the 
    //Requiredattribute.

    public class Employeebbbbbbbbbbbbbbvvvvvvvvvvvvvv
    {
        public int EmployeeID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }
    }


    //In the above example, the Name property is decorated with Required Attribute.
    //This will create the Name column as Not Null in the database. 
    //Without the Required attribute, all string properties are mapped to 
    //NULLABLE columns (Example Address in the above model)

    //ASP.NET MVC also uses this attribute to Validate the model in the User interface


}
