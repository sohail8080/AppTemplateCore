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



}
