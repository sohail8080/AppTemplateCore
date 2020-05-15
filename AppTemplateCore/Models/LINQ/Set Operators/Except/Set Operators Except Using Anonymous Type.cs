using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Except.AnonymousType
{
    //In this approach, we need to select all the individual properties to an anonymous type. 

    class Program22
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

           
            var MS = AllStudents.Select(x => new { x.ID, x.Name })
                    .Except(Class6Students.Select(x => new { x.ID, x.Name })).ToList();
           
            var QS = (from std in AllStudents
                      select new { std.ID, std.Name })
                      .Except(Class6Students.Select(x => new { x.ID, x.Name })).ToList();

            foreach (var student in QS)
            {
                Console.WriteLine($" ID : {student.ID} Name : {student.Name}");
            }

            Console.ReadKey();
        }
    }


    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }





}
