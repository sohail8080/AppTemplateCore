using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations.Database_DA
{

    //Index Attribute
    //Entity Framework 6 provides the[Index] attribute to create an index on a particular column 
    //in the database, as shown below:

    class Studenthhhhhffff
    {
        public int Student_ID { get; set; }
        public string StudentName { get; set; }

        //[Index]
        public int RegistrationNumber { get; set; }

        //[Index("INDEX_REGNUM", IsClustered = true, IsUnique = true)]
        public int RegistrationNo { get; set; }

    }

    //By default, the index name will be IX_ { property name }. However, 
    //you can change the index name.

    //You can also make it a clustered index by specifying IsClustered = true or create a unique 
    //index by specifying IsUnique = true.

    //Index Attribute

    //An index data annotation attribute is introduced with Entity Framework 6.1. The property of the model can be marked with an attribute and it should participate in a store index.The Index attribute allows us to create an index on one or more columns and we can specify the name of the index.

    //Example

    //[Index("IX_Name_DepartmentMaster", IsClustered = false)]
    //public string Name { get; set; }




    //    Index
    //The Index attribute was introduced in Entity Framework 6.1. If you are using an earlier version, the information in this section does not apply.

    //You can create an index on one or more columns using the IndexAttribute.

    //Adding the attribute to one or more properties will cause EF to create the corresponding index in the database when it creates the database.

    //Indexes make the retrieval of data faster and efficient, in most cases.However, overloading a table or view with indexes could unpleasantly affect the performance of other operations such as inserts or updates.

    //Indexing is the new feature in Entity Framework where you can improve the performance of your Code First application by reducing the time required to query data from the database.

    //You can add indexes to your database using the Index attribute, and override the default Unique and Clustered settings to get the index best suited to your scenario.

    //By default, the index will be named IX_<property name>

    //Let’s take a look at the following code in which Index attribute is added in Course class for Credits.

    //public class Course
    //    {
    //        public int CourseID { get; set; }
    //        public string Title { get; set; }
    //        [Index]
    //        public int Credits { get; set; }

    //        public virtual ICollection<Enrollment> Enrollments { get; set; }
    //    }
    //    You can see that the Index attribute is applied to the Credits property.When the table is generated, you will see IX_Credits in Indexes.

    //    By default, indexes are non-unique, but you can use the IsUnique named parameter to specify that an index should be unique.The following example introduces a unique index as shown in the following code.

    //public class Course
    //    {
    //        public int CourseID { get; set; }
    //        [Index(IsUnique = true)]

    //        public string Title { get; set; }
    //        [Index]

    //        public int Credits { get; set; }
    //        public virtual ICollection<Enrollment> Enrollments { get; set; }
    //    }


}
