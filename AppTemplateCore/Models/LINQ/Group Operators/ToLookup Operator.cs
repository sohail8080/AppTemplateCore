using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.LookupOperator
{
    //The Linq ToLookup Method in C# exactly does the same thing as the GroupBy Operator does in Linq. 
    //The only difference between these two methods is the GroupBy method uses deferred execution 
    //whereas the execution of the ToLookup method is immediate. 

    //The following example uses the ToLookup Method to organize the students into groups based on 
    //Branch as the key. Here, the key will be branch and the collection will be the student belongs 
    //to that particular branch.


    class Program22
    {
        static void Main22(string[] args)
        {
            var students = Student.GetStudents();


            var GroupByMS = students.ToLookup(s => s.Barnch);            
            var GroupByQS = (from std in students select std).ToLookup(x => x.Barnch);

            
            foreach (var group in GroupByMS)
            {
                Console.WriteLine(group.Key + " : " + group.Count());
                //Iterate through each student of a group
                foreach (var student in group)
                {
                    Console.WriteLine("  Name :" + student.Name + ", Age: " + student.Age + ", Gender :" + student.Gender);
                }
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
