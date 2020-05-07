using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    //NOT MAPPED TO THE DATABASE

    //Data Annotations - NotMapped Attribute in EF 6 & EF Core
    //The NotMapped attribute can be applied to properties of an entity class 
    //for which we do not want to create corresponding columns in the database.

    //By default, EF creates a column for each property (must have get; & set;) in an entity class. 
    //The [NotMapped] attribute overrides this default convention.

    //You can apply the [NotMapped] attribute on one or more properties for which 
    //you do NOT want to create a corresponding column in a database table.

    //NotMapped Attribute: [NotMapped()]

    public class StudentAAAS
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        [NotMapped]
        public int Age { get; set; }
    }


    //EF also does not create a column for a property which does not have either getters or setters.
    //For example, EF will not create columns for the following City and Age properties.



    public class StudentWWWWW
    {
        private int _age = 0;// NO COLUMN WILL BE CREATED

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string City { get { return StudentName; } }

        public int Age { set { _age = value; } }// NO COLUMN WILL BE CREATED
    }


    //Data Annotations NotMapped Attribute EF 6 & EF Core
    //Apply the NotMapped attribute on those properties, which you do not want to include 
    //in your database table.This attribute usually used in the case of calculated fields 
    //like Age, Amount, etc.This attribute is available both in 
    //Entity Framework & Entity Framework Core.This attribute is similar to the 
    //Fluent API Ignore method in EF Core.


    //NotMapped Attribute
    //Entity Framework conventions or Entity Framework Core Conventions creates the table column for every public property which has getters & setters.This behavior can be overridden using the NotMapped attribute

    //This attribute is from the System.ComponentModel.DataAnnotations.Schema Namespace

    //In the following example, we have included age property.We can calculate the age property from the DOB Property.Hence no need to create the database column for the age property

    public class Employee
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public DateTime DOB { get; set; }

        [NotMapped]
        public int Age { get; set; }
    }

    //The above model maps to the database as shown below.Tne EF will not create the Age column in the database, because of the NotMapped attribute on the property. Without this attribute, the entity framework will create the column in the database

    //NotMapped Attribute on tables
    //You can also apply the NotMapped attribute on the class itself. This will exlcude the model from the database


    [NotMapped]
    public class Employeeaaaaaaaaaaaaaaaac
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public DateTime DOB { get; set; }
    }


    [NotMapped]
    public class Employee22222222222222f
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public DateTime DOB { get; set; }
    }

}
