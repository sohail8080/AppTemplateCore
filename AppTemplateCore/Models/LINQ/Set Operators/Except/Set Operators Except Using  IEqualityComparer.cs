using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Exception.Compartor
{
    
    class Program2
    {
        static void Main22(string[] args)
        {

            List<Student> AllStudents = new List<Student>()
            {
                new Student {ID = 101, Name = "Preety" },
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 103, Name = "Hina"},
                new Student {ID = 104, Name = "Anurag"},
                new Student {ID = 105, Name = "Pranaya"},
                new Student {ID = 106, Name = "Santosh"},
            };

            List<Student> Class6Students = new List<Student>()
            {
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 104, Name = "Anurag"},
                new Student {ID = 105, Name = "Pranaya"},
            };

            //Create an instance of StudentComparer
            StudentComparer studentComparer = new StudentComparer();
           
            var MS = AllStudents.Except(Class6Students, studentComparer).ToList();
           
            var QS = (from std in AllStudents select std)
                      .Except(Class6Students, studentComparer).ToList();

        }
    }



    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }



    public class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
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
        public int GetHashCode(Student obj)
        {
            //If obj is null then return 0
            if (obj == null)
            {
                return 0;
            }
            //Get the ID hash code value
            int IDHashCode = obj.ID.GetHashCode();
            //Get the Name HashCode Value
            int NameHashCode = obj.Name == null ? 0 : obj.Name.GetHashCode();
            return IDHashCode ^ NameHashCode;
        }
    }



}
