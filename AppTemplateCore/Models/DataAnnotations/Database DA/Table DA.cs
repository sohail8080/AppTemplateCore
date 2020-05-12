using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    // Data Annotations - Table Attribute in EF 6 & EF Core
    //The Table attribute can be applied to a class to configure the corresponding table name 
    //in the database.It overrides the default convention in EF 6 and EF Core.
    //As per the default conventions, EF 6 creates a table name 
    //matching with <DbSet<TEntity> PROPERTY NAME + 's' (or 'es') in a context class 
    //and EF CORE creates the Db column with the same name as DbSet<TEntity> PROPERTY NAME.

    //Table Attribute: [Table(string name, Properties:[Schema = string])

    //name: Name of the Db table.
    //Schema: Name of the Db Schema in which a specified table should be created. (Optional)

    using System.ComponentModel.DataAnnotations.Schema;

    // TABLE NAME
    [Table("StudentMaster")]
    public class Student
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }
    }


    // SCHEMA
    [Table("StudentMaster", Schema = "Admin")]
    public class Studentss
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }
    }



    public class Customer
    {
        public int CustomerID { get; set; }

        string Name { get; set; }
    }

    //Add this in Context class
    //public DbSet<Customer> Customer { get; set; }

    //EF 6 ==> Customers
    //EF Core ==> Customer

    // TABLE NAME
    [Table("CustomerMaster")]
    public class Customervvvfff
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }
    }

    // TABLE NAME & SCHEMA
    [Table("CustomerMaster", Schema = "Admin")]
    public class Customervvvvvvvvvvvvv
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }
    }





    //Table Attribute

    //Table attributes specify the name of table and define the schema.This means that this attribute 
    //can be used when the entity name and table name in the database are different.


    //Table("Department", Schema = "dbo")]  
    //public class DepartmentMaster
    // {

    // }

    //In the preceding example, my entity name is “DepartmentMaster” and my database table name is 
    //“Department”.




    //    Table
    //Default Code First convention creates a table name similar to the class name. If you are letting Code First create the database, and also want to change the name of the tables it is creating.Then −

    //You can use Code First with an existing database.But it's not always the case that the names of the classes match the names of the tables in your database.

    //Table attribute overrides this default convention.

    //EF Code First will create a table with a specified name in Table attribute for a given domain class.

    //Let’s take a look at the following example in which the class is named Student and by convention, Code First presumes this will map to a table named Students.If that's not the case, you can specify the name of the table with the Table attribute as shown in the following code.

    //[Table("StudentsInfo")]
    //public class Student
    //    {

    //        [Key]
    //        public int StdntID { get; set; }
    //        [Required]
    //        public string LastName { get; set; }
    //        [Required]
    //        public string FirstMidName { get; set; }
    //        public DateTime EnrollmentDate { get; set; }

    //        public virtual ICollection<Enrollment> Enrollments { get; set; }
    //    }
    //    Now you can see that Table attribute specifies the table as StudentsInfo.When the table is generated, you will see the table name StudentsInfo as shown in the following image.


//    You cannot only specify the table name but you can also specify a schema for the table using Table attribute as shown in the following code.

//[Table("StudentsInfo", Schema = "Admin")]
//public class Student
//    {

//        [Key]
//        public int StdntID { get; set; }
//        [Required]
//        public string LastName { get; set; }
//        [Required]
//        public string FirstMidName { get; set; }
//        public DateTime EnrollmentDate { get; set; }

//        public virtual ICollection<Enrollment> Enrollments { get; set; }
//    }
//    You can see in the above example, the table is specified with admin schema.Now Code First will create StudentsInfo table in Admin schema as shown in the following image.









}
