using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    //Data Annotations - MaxLength Attribute in EF 6 & EF Core
    //The MaxLength attribute specifies the maximum length of data value allowed for 
    //a property which in turn sets the size of a corresponding column in the database.
    //It can be applied to the string or byte[] properties of an entity.


    //In the above example, the MaxLength(50) attribute is applied on the StudentName 
    //property which specifies that the value of StudentName property cannot be more 
    //than 50 characters long. 
    //This will create a StudentName column with nvarchar(50) size in the SQL Server database,

    using System.ComponentModel.DataAnnotations;

    public class Studentxxxxxxxxxx
    {
        public int StudentID { get; set; }

        [MaxLength(50)]
        public string StudentName { get; set; }

    }

    //Data Annotations MaxLength/MinLength Attribute in EF 6 & EF Core

    //The MaxLength Attribute and MinLength Attribute allow us to specify the size of 
    //the column.Both these attributes are available in 
    //System.ComponentModel.DataAnnotations library.
    //These attributes are similar to StringLength Attribute. 
    //Both of these attributes are available in EF 6 & EF Core.

    //MaxLength Attribute
    //MaxLength attribute used to set the size of the database column.
    //We can apply this attribute only to the string or array type properties of an entity.
    //The following example shows how to use the MaxLength Attribute


    public class EmployeeDSSSSS
    {
        public int EmployeeID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }

    public class EmployeeOUUUU
    {
        public int EmployeeID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }

    //MinLength Attribute
    //MinLength attribute used to specify the minimum length of string or an array property.
    //It is a validation attribute as it does not change the database schema.
    //The following example shows how to use MinLength attribute

    //[MinLength(10)]
    //public string Address { get; set; }


    // MaxLength and MinLength attribute can be used together as shown below.
    //In this example, Name cannot be greater than 50 characters and cannot be less 
    //than 10 characters


    //[MaxLength(50), MinLength(10)]
    //public string Name { get; set; }


    //[MaxLength(50), MinLength(10)]
    // public string Name { get; set; }

    // You can customize the validation error message displayed to the user using the Error
    //Message parameter of both MaxLength and MinLength attribute as shown below.


    public class Customer
    {
        public int CustomerID { get; set; }

        [MaxLength(50, ErrorMessage = "Name cannot be greater than 50")]
        public string Name { get; set; }

        [MinLength(10, ErrorMessage = "Name cannot be less than 10")]
        public string Address { get; set; }
    }

    //Entity framework will throw a VALIDATION ERRO0R(EntityValidationErrorMaxLength or 
    //size is less than a Minlength attribute.

    //StringLength Attribute
    //The Entity Framework also supports StringLength attribute, 
    //which is used by the ASP.NET MVC for validating the model.
    //This attribute is covered in the next tutorial StringLength attribute.

}
