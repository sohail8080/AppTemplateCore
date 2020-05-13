using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Distinct.Distinct.IEqualityComparer
{

    //Distinct: We need to use the Distinct() method when we want to remove the duplicate data or 
    //records from a data source. This method operates on a single data source.
    //If we need to select the distinct records from a data source(No Duplicate Records) then 
    //we need to use Set Operators.

    //IST APPROACH: IEqualityComparer
    //We need to use the other overloaded version of Distinct() method which takes 
    //IEqualityComparer interface as an argument.
    //So here we will create a class that implements IEqualityComparer interface and then 
    //we need to pass that compare instance to the Distinct() method.

    class Program4445
    {
        static void Main(string[] args)
        {
            var students = Studentppp.GetStudents();

            //Creating an instance of StudentComparer
            StudentComparer studentComparer = new StudentComparer();
            var MS54 = students.Distinct(studentComparer).ToList();
            var QS54 = (from std in students select std).Distinct(studentComparer).ToList();
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


    public class StudentComparer : IEqualityComparer<Studentppp>
    {

        public bool Equals(Studentppp x, Studentppp y)
        {
            //First check if both object reference are equal then return true
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            //If either one of the object refernce is null, return false
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }
            //Comparing all the properties one by one
            return x.ID == y.ID && x.Name == y.Name;
        }


        public int GetHashCode(Studentppp obj)
        {
            //If obj is null then return 0
            if (obj == null)
            {
                return 0;
            }
            //Get the ID hash code value
            int IDHashCode = obj.ID.GetHashCode();
            //Get the string HashCode Value
            //Check for null refernece exception
            int NameHashCode = obj.Name == null ? 0 : obj.Name.GetHashCode();
            return IDHashCode ^ NameHashCode;
        }


    }




}
