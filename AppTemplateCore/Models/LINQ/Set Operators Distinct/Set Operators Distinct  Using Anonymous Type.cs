using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Distinct.NewAnonymousType
{

    // 3RD APPROACH: new anonymous type
    //In the third approach, we need to project the required properties into a new anonymous type, 
    //which already overrides the Equals() and GetHashCode() methods 
    //Here we need to project the properties of Student class into a new anonymous type, 
    //which already overrides the Equals() and GetHashCode() methods.
    //In the above example, we project the ID and Name properties to IEnumeable<’a> means to anonymous type which 
    //already overrides the Equals and GetHashCode method. 
    //Now run the application and you will see the output as expected.


    class Program4445
    {
        static void Main(string[] args)
        {
            var students = Studentppp.GetStudents();

            var MS999 = students.Select(std => new { std.ID, std.Name }).Distinct().ToList();
            var QS9999 = (from std in students select std).Select(std => new { std.ID, std.Name }).Distinct().ToList();


        }
    }


    public class Studentppp
    {
        public int ID { get; set; }

        public string Name { get; set; }


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
