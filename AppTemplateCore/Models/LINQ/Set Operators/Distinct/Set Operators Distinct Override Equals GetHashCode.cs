using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Distinct.OverrideEqualsGetHashCode
{

    //Distinct: We need to use the Distinct() method when we want to remove the duplicate 
    //data or records from a data source. This method operates on a single data source.
    //If we need to select the distinct records from a data source(No Duplicate Records) then we
    //need to use Set Operators.

    //2ND APPROACH: Override Equals() GetHashCode()
    //In the second approach, we need to override the Equals() and GetHashCode() methods within the 
    //Student class itself. 


    class Program4445
    {
        static void Main2(string[] args)
        {
            var students = Studentppp.GetStudents();


            var MS877 = students.Distinct().ToList();
            var QS988 = (from std in students select std).Distinct().ToList();


        }
    }


    public class Studentppp
    {
        public int ID { get; set; }

        public string Name { get; set; }


        public override bool Equals(object obj)
        {
            //As the obj parameter type id object, so we need to
            //cast it to Student Type
            return this.ID == ((Studentppp)obj).ID && this.Name == ((Studentppp)obj).Name;
        }


        public override int GetHashCode()
        {
            //Get the ID hash code value
            int IDHashCode = this.ID.GetHashCode();
            //Get the string HashCode Value
            //Check for null refernece exception
            int NameHashCode = this.Name == null ? 0 : this.Name.GetHashCode();
            return IDHashCode ^ NameHashCode;
        }


        public static List<Studentppp> GetStudents()
        {
            List<Studentppp> students = new List<Studentppp>()
            {
                new Studentppp {ID = 101, Name = "Preety" },
                new Studentppp {ID = 102, Name = "Sambit" },
                new Studentppp {ID = 103, Name = "Hina"},
                new Studentppp {ID = 104, Name = "Anurag"},
                new Studentppp {ID = 102, Name = "Sambit"},
                new Studentppp {ID = 103, Name = "Hina"},
                new Studentppp {ID = 101, Name = "Preety" },
            };

            return students;
        }
    }


}
