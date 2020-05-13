using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.GroupByMultipleKeys
{

    //Why do we need to group the data based on Multiple keys?
    //In real-time applications, we need to group the data based on multiple keys.
    //So in this article, we will see how to group the data based on multiple keys. 
    //when you are using multiple keys in Group By operator then 
    //the data returned is an anonymous type.

    //The following example group the students first by Branch and then by Gender. 
    //The student groups first sorted by Branch in descending order and then by Gender in ascending order.
    //Finally, the students in each group are sorted by their names in ascending order. 


    class Program
    {

        

        static void Main(string[] args)
        {
            var students = Student.GetStudents();

            var GroupByMultipleKeysMS = students
                                        .GroupBy(x => new { x.Barnch, x.Gender })
                                        .OrderByDescending(g => g.Key.Barnch).ThenBy(g => g.Key.Gender)
                                        .Select(g => new
                                        {
                                            Branch = g.Key.Barnch,
                                            Gender = g.Key.Gender,
                                            Students = g.OrderBy(x => x.Name)
                                        });
           
            var GroupByMultipleKeysQS = from student in students
                                        group student by new
                                        {
                                            student.Barnch,
                                            student.Gender
                                        } into stdGroup
                                        orderby stdGroup.Key.Barnch descending,
                                                stdGroup.Key.Gender ascending
                                        select new
                                        {
                                            Branch = stdGroup.Key.Barnch,
                                            Gender = stdGroup.Key.Gender,
                                            Students = stdGroup.OrderBy(x => x.Name)
                                        };
            

            foreach (var group in GroupByMultipleKeysQS)
            {
                Console.WriteLine($"Barnch : {group.Branch} Gender: {group.Gender} No of Students = {group.Students.Count()}");
               
                foreach (var student in group.Students)
                {
                    Console.WriteLine($"  ID: {student.ID}, Name: {student.Name}, Age: {student.Age} ");
                }
                Console.WriteLine();
            }
            Console.Read();
        }
    }


    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Barnch { get; set; }
        public int Age { get; set; }
        public static List<Student> GetStudents()
        {
            return new List<Student>()
        {
            new Student { ID = 1001, Name = "Preety", Gender = "Female",
                                         Barnch = "CSE", Age = 20 },
            new Student { ID = 1002, Name = "Snurag", Gender = "Male",
                                         Barnch = "ETC", Age = 21  },
            new Student { ID = 1003, Name = "Pranaya", Gender = "Male",
                                         Barnch = "CSE", Age = 21  },
            new Student { ID = 1004, Name = "Anurag", Gender = "Male",
                                         Barnch = "CSE", Age = 20  },
            new Student { ID = 1005, Name = "Hina", Gender = "Female",
                                         Barnch = "ETC", Age = 20 },
            new Student { ID = 1006, Name = "Priyanka", Gender = "Female",
                                         Barnch = "CSE", Age = 21 },
            new Student { ID = 1007, Name = "santosh", Gender = "Male",
                                         Barnch = "CSE", Age = 22  },
            new Student { ID = 1008, Name = "Tina", Gender = "Female",
                                         Barnch = "CSE", Age = 20  },
            new Student { ID = 1009, Name = "Celina", Gender = "Female",
                                         Barnch = "ETC", Age = 22 },
            new Student { ID = 1010, Name = "Sambit", Gender = "Male",
                                         Barnch = "ETC", Age = 21 }
        };
        }
    }



}
