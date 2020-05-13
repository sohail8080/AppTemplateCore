using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.GroupBy
{
    //The Linq GroupBy Method in C# belongs to the Grouping Operators category and 
    //exactly does the same thing as the Group By clause does in SQL Query. 
    //This method takes a flat sequence of elements and then organizes the elements into groups 
    //(i.e. IGrouping<TKey, TSource>) based on a given key.


    class Program
    {
        static void Main(string[] args)
        {
            var students = Student.GetStudents();


            var GroupByMS = students.GroupBy(s => s.Barnch);
           
            IEnumerable<IGrouping<string, Student>> GroupByQS = 
                (from std in students group std by std.Barnch);



            var GroupByMS2 = students.GroupBy(s => s.Gender)
                            //First sorting the data based on key in Descending Order
                            .OrderByDescending(c => c.Key)
                            .Select(std => new
                            {
                                Key = std.Key,
                                //Sorting the data based on name in descending order
                                Students = std.OrderBy(x => x.Name)
                            });

            
            var GroupByQS2 = from std in students
                             group std by std.Gender into stdGroup
                            orderby stdGroup.Key descending
                            select new
                            {
                                Key = stdGroup.Key,
                                Students = stdGroup.OrderBy(x => x.Name)
                            };


            //  Each group has a key and you can access the key-value by using the key property. 
            //Along the same line, you can use the count property to check how many elements are
            //there in that group.

            foreach (var group in GroupByMS)
            {
                Console.WriteLine(group.Key + " : " + group.Count());
               
                foreach (var student in group)
                {
                    Console.WriteLine("  Name :" + student.Name + ", Age: " + student.Age + ", Gender :" + student.Gender);
                }
            }
           
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
