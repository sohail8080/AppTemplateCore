﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    //Data Annotations - DatabaseGenerated Attribute in EF 6 & EF Core

    //As you know, EF creates an IDENTITY column in the database for all the id(key) properties 
    //of the entity, by default. 
    //So, the underlying database generates a value for this column on each insert command, 
    //e.g., SQL Server creates an integer IDENTITY column with identity seed and increment to 1.

    //EF 6 and EF Core provide the DatabaseGenerated data annotation attribute 
    //to configure how the value of a property will be generated. 
    // CONFIQURE HOW THE VALUE OF THE PROPERTY WILL BE GENERATED.

    //The DatabaseGenerated attribute takes one out of the following three 
    //DatabaseGeneratedOption enum values :

    //DatabaseGeneratedOption.None
    //DatabaseGeneratedOption.Identity
    //DatabaseGeneratedOption.Computed

    //DatabaseGeneratedOption.None
    //DatabaseGeneratedOption.None option specifies that the value of a property 
    //will not be generated by the underlying database. 

    //This will be useful to override the default convention for the id properties.

    //For example, if you want to provide your own values to id properties 
    //instead of database generated values, use the None option, as shown below.


    public class Coursebbbbbb
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        public string CourseName { get; set; }
    }

    //In the above example, EF will create the CourseId column in the database and will not mark 
    //it as an IDENTITY column.So, each time you will have to provide the value of 
    //the CourseId property before calling the SaveChanges() method.

    //using (var context = new SchoolContext())
    //{
    //you must provide the unique CourseId value
    //var maths = new Course() { CourseId = 1, CourseName = "Maths" };
    //context.Courses.Add(maths);

    //you must provide the unique CourseId value
    //var eng = new Course() { CourseId = 2, CourseName = "English" };
    //context.Courses.Add(eng);

    //the following will throw an exception as CourseId has duplicate value
    //var sci = new Course(){ CourseId=2,  CourseName="sci"};

    //context.SaveChanges();
    //}

    //Note: EF will throw an exception if you do not provide unique values each time because 
    //CourseId is a primary key property.

    //Use the ValueGeneratedNever() method of Fluent API to specify an Identity property in EF Core, 
    //as shown below.

    //modelBuilder.Entity<Course>()
    //.Property(p => p.CourseId)
    //.ValueGeneratedNever();

    ////////////////////////////////////////////////////////////////////////////

    // Data Annotations DatabaseGenerated Attribute in EF 6 & EF Core

    //Use DatabaseGenerated Attribute on a property whose value is automatically generated by 
    //the Database. This attribute works both in Entity framework & EF Core.

    //DatabaseGenerated Attribute
    //You may have computed fields in your database.The database updates the computed fields 
    //when you insert or update the data. 
    //For Example, database inserts the new values for Identity or Guid columns, when you insert the data.
    //The inserted data must be updated in the model, once the saveChanges is called

    //DatabaseGeneratedOption
    //This attribute has three options

    //Identity: The database generates value when we insert the row.
    //Computed: Database generates a value for the property when we insert or update the row.
    //None: Database does not generate a value for the property either in insert or in an update.

    //Note that how DatabaseGenerated implemented differs from the one database provider 
    //to another database provider.The Example in this article uses the SQL Server

    //DatabaseGenerated.None
    //This prevents the database from creating the computed values.The user must provide the value.

    //This is useful when you want to disable the generation of identity for the integer primary key. 
    //The following domain model Customer, with the DatabaseGeneratedOption.None attribute will create 
    //the CustomerID column with identity disabled.

    public class Customer2222222222222222
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }

        public int SrNo { get; set; }

        public string Test { get; set; }

        public string CustomerName { get; set; }

        public DateTime? Created { get; set; } = DateTime.UtcNow;

    }


}