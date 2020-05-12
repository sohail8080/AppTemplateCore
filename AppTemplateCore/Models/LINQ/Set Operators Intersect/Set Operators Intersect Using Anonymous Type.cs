using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Intersect.Annonymouse
{
    //Using Anonymous Type for Intersect Operation in C#:
    //In this approach, we need to select all the individual properties to an anonymous type. 

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


            var MS = s1.Select(x => new { x.ID, x.Name })
                     .Intersect(s2.Select(x => new { x.ID, x.Name })).ToList();


            var QS = (from std in s1
                      select new { std.ID, std.Name })
                      .Intersect(s2.Select(x => new { x.ID, x.Name })).ToList();

        }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

}
