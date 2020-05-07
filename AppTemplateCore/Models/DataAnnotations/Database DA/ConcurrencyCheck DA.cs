using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{
    // ConcurrencyCheck COLUMN, FOR AUTOMATIC CONCURRENTCY CHECKS IN UPDATE/DELETE STATEMENTS WHERE CLAUSE.
    // NOT RELATED TO CREATE OR SELECT STATEMENTS
    // CAN BE APPLIED TO MULTIPLE COLUMNS IN TABLE, SEAMLESS CONCURRENTCY CHECKS. 
    // DONE BEHIND THE SCENE BY EF.
    // ALL WE HAVE TO DO IS TO CREATE THIS COLUMN AND USE MIGRATION TO GENERATE THAT TABLE.
    // HOW THE ERROR HANDLING WILL BE DONE WHEN CONFLICY ORRCURES. IS IT DONE AUTO WITH EF.
    // TIMESTAMP DATA ANNOTATION IS PREFERRED OVER CONCURRENCYCHECK DATA ANNOTATION
    // THIS COLUMN CONTAINS USER DATA. USER DATA COLUMN FOR CONCURERNCY CHECKS.
    // BOTH PROPERTIES THOSE ARE UPDATED/NOT UPDATED IN THE UPDATE OPERATION CAN BE USED 
    // AS THE ConcurrencyCheck ATTRIBUTE. 
    // OLD VALUE @ QUERYING TIME VALUE AND CURRENT VALUE IN DB ARE COMPARED IF BOTH ARE EQUAL
    // THEN NEW VALUE IS UPDATED. OTHER WISE NOT. SOMEONE ELSE HAS CHANGED THE ROW AFTER YOU FETCH THE 
    // RECORD. TRADTIONALL WE USE ROWCOUND FOR THIS. EF PROVIDED THIS AUTOMATICALLY.
    // WE DO NOT HAVE TO BOTHER THE COMPARE, INCREMENT, QUERYT THE ROWCOUNTER DURING OPERATIONS.
    // WHEN CONCURRENCY CHECKS FAILS, THE EXCEPTION OCCURS, THIS EXCETOIN SHOULD BE HANDLED 
    // SEPERATETLY AND USER SHOLD BE INTIMATED TO RELAOD THE DATA AND APPLY CHANGED TO NEW DATA IN TABLE
    //================================================================================

    //Data Annotation - ConcurrencyCheck Attribute in EF 6 & EF Core
    //The ConcurrencyCheck attribute can be applied to ONE OR MORE PROPERTIES in an entity class 
    //in EF 6 and EF Core.

    //When applied to a property, the corresponding column in the database table will be used 
    //in the OPTIMISTIC CONCURRENCY CHECKS using the WHERE CLAUSE.

    using System.ComponentModel.DataAnnotations;

    public class Student321654
    {
        public int StudentId { get; set; }

        [ConcurrencyCheck]
        public string StudentName { get; set; }// WILL PARTICIPATE IN COCURRENTY CHECKS
    }

    //In the above example, the ConcurrencyCheck attribute is applied to the StudentName property 
    //of the Student entity class. 

    //So, EF will include the StudentName column in the UPDATE statement WHERE CLAUSE to check for 
    //OPTIMISTIC CONCURRENCY.Consider the following example.

    //    using(var context = new SchoolContext()) 
    //{
    //    var std = new Student()
    //    {
    //        StudentName = "Bill"
    //    };

    //    context.Students.Add(std);
    //    context.SaveChanges();

    //    std.StudentName = "Steve";
    //    context.SaveChanges();// AUTO & SEAMLESS CONCURRENCY CHECKS BY OR MAPPER.
    //}

    //The above example will execute the following UPDATE statement on SaveChanges(), 
    //where it includes StudentName in the where clause.

    //exec sp_executesql N'UPDATE [dbo].[Students]
    //SET[StudentName] = @0
    //WHERE(([StudentId] = @1) AND([StudentName] = @2))
    //',N'@0 nvarchar(max) ,@1 int,@2 nvarchar(max) ',@0=N'Steve',@1=1,@2=N'Bill'
    //go

    //Note: The Timestamp attribute can only be applied to a SINGLE BYTE ARRARY PROPERTY, 
    //whereas the ConcurrencyCheck attribute can be applied to ANY NUMBER OF PROPERTIES 
    //with any data type.

    ///////////////////////////////////////////////////////////////////

    //Concurrency Check used to handle conflicts that result when multiple users are UPDATING (or DELETING) 
    //the table at the same time. 

    //You can add the ConcurrencyCheck attribute on ANY PROPERTY AND ANY NUMBER OF PROPERTIES, 
    //which you want to participate in the Concurrency Check.

    //This attribute works the same way in both EF 6 & EF Core.

    //What is Concurrency check
    //Assume that two users simultaneously query for same data to edit from the Employee Table.
    //One of the users saves his changes. Now, the other user is now looking at the data, 
    //which is invalid.

    //If he also modifies the data and saves it, it will overwrite the first user’s changes. 
    //What if both users save the data at the same time.We never know which data gets saved.

    //We use the Concurrency check precisely to avoid such situations.
    //To do that we include ADDITIONAL FIELDS in the where clause apart from the primary key. 
    //For Example, by including the name field in the where clause, we are ensuring that the 
    //value in the name field has NOT CHANGED since we LAST QUERIED IT.
    //If someone has changed the field, then the where clause fails the Entity Framework 
    //RAISES THE EXCEPTION

    //ConcurrencyCheck Attribute
    //This attribute resides in the System.ComponentModel.DataAnnotations namespace


    public class EmployeeXXXXXXXXXXXXXX
    {
        public int EmployeeID { get; set; }

        [ConcurrencyCheck]
        public string Name { get; set; }// DATA FILED ALONG WITH CONCURRENCY

        public string Address { get; set; }
    }


    public class Employeemmmmmmmmmmmmmmmmmmm
    {
        public int EmployeeID { get; set; }

        [ConcurrencyCheck]
        public string Name { get; set; }// DATA FILED ALONG WITH CONCURRENCY

        public string Address { get; set; }
    }

    //In the above example, We decorate the Name Property with the ConcurrencyCheck attribute.
    //When ENITITY FRAMEWORK generates the UPDATE OR DELETE statement, it always includes the 
    //Name column in WHERE CLAUSE.

    //This attribute DOES NOT AFFECT THE DATA BASE MAPPINGS in any way. 
    //This attribute is similar to timestamp attribute.

    //You can apply ConcurrencyCheck Attribute on ANY NUMBER OF PROPERTIES.
    //There is NO RESTRICTION ON DATA TYPE for this attribute




}
