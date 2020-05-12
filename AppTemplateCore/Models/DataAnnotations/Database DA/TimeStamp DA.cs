using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    // TIMESTAMP COLUMN, FOR AUTOMATIC CONCURRENTCY CHECKS INT UPDATE STATEMENTS WHERE CLAUSE.
    // APPLIED ONLY ONE COLUMN IN TABLE, SEAMLESS CONCURRENTCY CHECKS. DONE BEHIND THE SCEME BY EF.
    // ALL WE HAVE TO DO IS TO CREATE THIS COLUMN AND USE MIGRATION TO GENERATE THAT TABLE.
    // HOW THE ERROR HANDLING WILL BE DONE WHEN CONFLICY ORRCURES. IS IT DONE AUTO WITH EF.
    // TIMESTAMP DATA ANNOTATION IS PREFERRED OVER CONCURRENCYCHECK DATA ANNOTATION
    // THIS COLUMN DO NOT CONTAIN USER DATA. RESERVE COLUMN FOR CONCURERNCY CHECKS
    // OLD VALUE @ QUERYING TIME VALUE AND CURRENT VALUE IN DB ARE COMPARED IF BOTH ARE EQUAL
    // THEN NEW VALUE IS UPDATED. OTHER WISE NOT. SOMEONE ELSE HAS CHANGED THE ROW AFTER YOU FETCH THE 
    // RECORD. TRADTIONALL WE USE ROWCOUND FOR THIS. EF PROVIDED THIS AUTOMATICALLY.
    // WE DO NOT HAVE TO BOTHER THE COMPARE, INCREMENT, QUERYT THE ROWCOUNTER DURING OPERATIONS.
    // WHEN CONCURRENCY CHECKS FAILS, THE EXCEPTION OCCURS, THIS EXCETOIN SHOULD BE HANDLED 
    // SEPERATETLY AND USER SHOLD BE INTIMATED TO RELAOD THE DATA AND APPLY CHANGED TO NEW DATA IN TABLE
    //================================================================================
    //Data Annotations - Timestamp Attribute in EF 6 & EF Core
    //EF 6 and EF Core both include the Timestamp data annotation attribute.
    //It can only be applied once in an entity class to a byte array type property.
    //It creates a COLUMN WITH TIMESTAMP DATATYPE in the SQL Server database. 
    //Entity Framework API AUTOMATICALLY USES this Timestamp column in CONCURRENTCY CHECKS 
    //on the UPDATE statement in the database.

    //This timestamp column will be included in the where clause whenever 
    //you update an entity and call the SaveChanges method.

    using System.ComponentModel.DataAnnotations;

    public class StudentNBVCX
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        [Timestamp]// BYTE ARRAY
        public byte[] RowVersion { get; set; }// ATTRIBUTE FOR CONCURRENCY CHECKS 
    }


    //    using(var context = new SchoolContext()) 
    //{
    //    var std = new Student()
    //    {
    //        StudentName = "Bill"
    //    };

    //    context.Students.Add(std);
    //    context.SaveChanges();// SEAMLESS CONCURENCY CHECKS.

    //    std.StudentName = "Steve";
    //    context.SaveChanges();
    //}


    //The above code will execute the following UPDATE statement in the database.

    //exec sp_executesql N'UPDATE [dbo].[Students]
    //SET[StudentName] = @0
    //WHERE(([StudentId] = @1) AND([RowVersion] = @2))
    //SELECT[RowVersion]
    //FROM[dbo].[Students]
    //    WHERE @@ROWCOUNT > 0 AND[StudentId] = @1',N'@0 nvarchar(max) ,@1 int,@2 binary(8)',@0=N'Steve',@1=1,@2=0x00000000000007D1
    //go

    //===============================================================

    //Data Annotations Timestamp Attribute in EF 6 & EF Core

    //Using Timestamp Attribute is a way to handle the CONCURRENCY ISSUES.
    //The Concurrency issue arises when multiple users attempt to update/delete the SAME ROW
    //at the same time.

    //This issue can be handled either by using the TIMESTAMP column or CONCURRENCYCHECK 
    //attribute on the property.

    //Timestamp columns are the preferred way of using for 
    //concurrency check.

    //This attribute is available in both Entity Framework & Entity Framework Core

    //You can apply Timestamp attribute to any byte array column as shown in the entity model 
    //below.The Attribute is applied to RowID Property. 

    //The entity framework AUTOMATICALLY adds the TimeStamp columns in update/delete queries.      

    public class Employeemmmmmmmmmm
    {
        public int EmployeeID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [Timestamp]// ROWVERSION USED FOR CONCURRENCTY
        public byte[] RowID { get; set; }
    }

    //We can apply Timestamp attribute to only ONE PROPERTY in the domain model.
    //The data type of the Timestamp must be a BYTE ARRAY
    //This attribute affects the database as it creates the column with datatype ROWVERSION. 
    //byte[] array without Timestamp creates the varninary(max) column
    //Timestamp columns are the preferred way of using for CONCURRENCY CHECKS


    //Timestamp Attribute

    //The Timestamp attribute specifies the byte array(byte[]) property / column that has a concurrency mode of "Fixed" in the model and it should be a Timestamp column in the stored model(database).

    //Example

    //[Timestamp]
    //public Byte[] TimeStamp { get; set; }






//    Timestamp
//Code First will treat Timestamp properties the same as ConcurrencyCheck properties, but it will also ensure that the database field that code first generates is non-nullable.

//It's more common to use rowversion or timestamp fields for concurrency checking.

//Rather than using the ConcurrencyCheck annotation, you can use the more specific TimeStamp annotation as long as the type of the property is byte array.

//You can only have one timestamp property in a given class.

//Let’s take a look at a simple example by adding the TimeStamp property to the Course class −

//public class Course
//    {

//        public int CourseID { get; set; }
//        public string Title { get; set; }
//        public int Credits { get; set; }
//        [Timestamp]
//        public byte[] TStamp { get; set; }

//        public virtual ICollection<Enrollment> Enrollments { get; set; }
//    }
//    As you can see in the above example, Timestamp attribute is applied to Byte[] property of the Course class. So, Code First will create a timestamp column TStamp in the Courses table.




}
