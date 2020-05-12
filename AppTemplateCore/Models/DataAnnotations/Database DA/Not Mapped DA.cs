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



    //NotMapped Attribute

    //In the Code First model approach, every property of the model is represented as a table's column in the database. This is not always the case, we might require some property in a model or entity that is not present in the database table. For example my Department entity has a property called DepartmentCodeName, this property returns a combination of code and name separated by a colon(:) . This property can be created dynamically and there is no need to store it into the database. We can mark this property with NotMapped annotation. In short if we decorate any property or class with this attribute then they should be excluded from database mapping.

    //Example

    public class DepartmentMaster
    {
        [NotMapped]
        public string DepartmentCodeName
        {
            get
            {
                return string.Empty;//Code + ":" + Name;
            }
        }

    }

    [NotMapped]
    public class InternalClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }




    //    NotMapped
    //By default conventions of Code First, every property that is of a supported data type and which includes getters and setters are represented in the database.But this isn’t always the case in your applications. NotMapped attribute overrides this default convention.For example, you might have a property in the Student class such as FatherName, but it does not need to be stored.You can apply NotMapped attribute to a FatherName property which you do not want to create a column of in the database as shown in the following code.

    //public class Student
    //    {
    //        [Key]
    //        public int StdntID { get; set; }
    //        public string LastName { get; set; }
    //        public string FirstMidName { get; set; }

    //        public DateTime EnrollmentDate { get; set; }
    //        [NotMapped]

    //        public int FatherName { get; set; }
    //        public virtual ICollection<Enrollment> Enrollments { get; set; }
    //    }
    //    You can see that NotMapped attribute is applied to the FatherName property.When the table is generated you will see that FatherName column will not be created in a database, but it is present in Student class.


    //Code First will not create a column for a property, which does not have either getters or setters as shown in the following example of Address and Age properties of Student class.



    //    InverseProperty
    //InverseProperty is used when you have multiple relationships between classes.In the Enrollment class, you may want to keep track of who enrolled a Current Course and Previous Course.Let’s add two navigation properties for the Enrollment class.

    //public class Enrollment
    //    {
    //        public int EnrollmentID { get; set; }
    //        public int CourseID { get; set; }
    //        public int StudentID { get; set; }
    //        public Grade? Grade { get; set; }

    //        public virtual Course CurrCourse { get; set; }
    //        public virtual Course PrevCourse { get; set; }
    //        public virtual Student Student { get; set; }
    //    }
    //    Similarly, you’ll also need to add in the Course class referenced by these properties.The Course class has navigation properties back to the Enrollment class, which contains all the current and previous enrollments.

    //public class Course
    //    {

    //        public int CourseID { get; set; }
    //        public string Title { get; set; }
    //        [Index]

    //        public int Credits { get; set; }
    //        public virtual ICollection<Enrollment> CurrEnrollments { get; set; }
    //        public virtual ICollection<Enrollment> PrevEnrollments { get; set; }
    //    }
    //    Code First creates {Class Name}
    //_{Primary Key} foreign key column, if the foreign key property is not included in a particular class as shown in the above classes.When the database is generated, you will see the following foreign keys.






}
