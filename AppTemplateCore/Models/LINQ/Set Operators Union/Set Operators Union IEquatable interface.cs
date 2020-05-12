using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Union.IEquatable
{

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> s1 = new List<Student>()
            {
                new Student {ID = 101, Name = "Preety" },
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 105, Name = "Hina"},
                new Student {ID = 106, Name = "Anurag"},
            };
            List<Student> s2 = new List<Student>()
            {
                new Student {ID = 105, Name = "Hina"},
                new Student {ID = 106, Name = "Anurag"},
                new Student {ID = 107, Name = "Pranaya"},
                new Student {ID = 108, Name = "Santosh"},
            };

           
            var MS = s1.Union(s2).ToList();
            var QS = (from std in s1 select std).Union(s2).ToList();

        }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }


}
