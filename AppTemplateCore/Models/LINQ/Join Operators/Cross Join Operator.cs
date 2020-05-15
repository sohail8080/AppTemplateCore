using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Join_Operators.CrossJoin
{

    //What is Linq Cross Join?
    //When combining two data sources(or you can two collections) using Linq Cross Join, 
    //then each element in the first collection will be mapped with each element in the second collection
    //Cross Join produces the Cartesian Products of the collections involved in the join.

    //In Cross Join we don’t require the common property as the “on” keyword, 
    //which is used to specify the Join Key, is not required. 
    //And moreover, there is no filtering of data. 
    //So, the total number of elements in the resultant sequence will be the 
    //product of the two data sources involved in the join. 
    //If the first data source contains 5 elements and the second data source contains 3 elements 
    //then the resultant sequence will contain (5*3) 15 elements.

    //Cross Join Using Query Syntax

    class Program22
    {
        static void Main22(string[] args)
        {
            var students = Student.GetAllStudnets();
            var subjects = Subject.GetAllSubjects();

            var CrossJoinResult = 
                from student in students from subject in subjects
                                  select new
                                  {
                                      Name = student.Name,
                                      SubjectName = subject.SubjectName
                                  };



            // //Cross Join using SelectMany Method
            var CrossJoinResult2 =  students.SelectMany(stu => subjects, (std, sub) => new
                         {
                             Name = std.Name,
                             SubjectName = sub.SubjectName
                         });

            ////Cross Join using Join Method
            var CrossJoinResult3 = students.Join(subjects, std => true, sub => true, (std, sub) => new
                            {
                                Name = std.Name,
                                SubjectName = sub.SubjectName
                            });




            foreach (var item in CrossJoinResult)
            {
                Console.WriteLine($"Name : {item.Name}, Subject: {item.SubjectName}");
            }
            
        }
    }


    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public static List<Student> GetAllStudnets()
        {
            return new List<Student>()
            {
                new Student { ID = 1, Name = "Preety"},
                new Student { ID = 2, Name = "Priyanka"},
                new Student { ID = 3, Name = "Anurag"},
                new Student { ID = 4, Name = "Pranaya"},
                new Student { ID = 5, Name = "Hina"}
            };
        }
    }
    public class Subject
    {
        public int ID { get; set; }
        public string SubjectName { get; set; }
        public static List<Subject> GetAllSubjects()
        {
            return new List<Subject>()
            {
                new Subject { ID = 1, SubjectName = "ASP.NET"},
                new Subject { ID = 2, SubjectName = "SQL Server" },
                new Subject { ID = 5, SubjectName = "Linq"}
            };
        }
    }

}
