using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.SelectMany
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Programming { get; set; }
        public static List<Student> GetStudents()
        {
            return new List<Student>()
            {
                new Student(){ID = 1, Name = "James", Email = "James@j.com", Programming = new List<string>() { "C#", "Jave", "C++"} },
                new Student(){ID = 2, Name = "Sam", Email = "Sara@j.com", Programming = new List<string>() { "WCF", "SQL Server", "C#" }},
                new Student(){ID = 3, Name = "Patrik", Email = "Patrik@j.com", Programming = new List<string>() { "MVC", "Jave", "LINQ"} },
                new Student(){ID = 4, Name = "Sara", Email = "Sara@j.com", Programming = new List<string>() { "ADO.NET", "C#", "LINQ" } }
            };
        }
    }



    class Program2
    {
        //The SelectMany in LINQ is used to project each element of a sequence to an IEnumerable<T> 
        //and then flatten the resulting sequences into one sequence.
        //That means the SelectMany operator combines the records from a sequence of results and then 
        //converts it into one result.

        static void Main(string[] args)
        {

            List<string> nameList = new List<string>() { "Pranaya", "Kumar" };

            List<char> methodSyntax = nameList.SelectMany(x => x).ToList();

            // Resutl: Pranaya Kumar

            // SelectMany method fetches all the characters from the above two strings and then converts it into one sequence 
            //i.e. IEnumerable<char>.

            List<Student> students = Student.GetStudents();

            // Programming is Child Table records with one to many relationships
            // We want to combine all the Child Records of all Master Rows in one array.
            // with duplicates strings
            List<string> MethodSyntax = students.SelectMany(std => std.Programming).ToList();
            List<string> QuerySyntax = (from std in students from program in std.Programming select program).ToList();

            // without duplicates but unique strings
            List<string> MethodSyntax1 = students.SelectMany(std => std.Programming).Distinct().ToList();
            List<string> QuerySyntax1 = (from std in students from program in std.Programming select program).Distinct().ToList();

            // Total 12 Child Records in the Programming Table
            // Result: Student Name + One Langauge, 12 Rows returned
            var MethodSyntaxbnb = students.SelectMany(std => std.Programming,
                                            (student, program) => new
                                            {
                                                StudentName = student.Name,
                                                ProgramName = program
                                            }).ToList();
            var QuerySyntaxssd = (from std in students from program in std.Programming
                                        select new
                                        {
                                            StudentName = std.Name,
                                            ProgramName = program
                                        }).ToList();




        }
    }





}
