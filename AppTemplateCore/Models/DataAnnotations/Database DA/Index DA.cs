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






}
