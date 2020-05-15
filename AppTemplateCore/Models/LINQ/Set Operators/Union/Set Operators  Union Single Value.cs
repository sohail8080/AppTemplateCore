using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Union.SingleValue
{
    //The LINQ Union Method in C# is used to combine the multiple data sources into 
    //one data source by removing the duplicate elements. 

    class Program22
    {
        static void Main22(string[] args)
        {
            List<int> dataSource1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            List<int> dataSource2 = new List<int>() { 1, 3, 5, 8, 9, 10 };
           
            var MS = dataSource1.Union(dataSource2).ToList();
            
            var QS = (from num in dataSource1 select num).Union(dataSource2).ToList();

            string[] dataSource11 = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] dataSource21 = { "India", "uk", "Canada", "France", "Japan" };
            
            var MS1 = dataSource11.Union(dataSource21).ToList();            
            var QS1 = (from country in dataSource11 select country).Union(dataSource21).ToList();


            string[] dataSource12 = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] dataSource22 = { "India", "uk", "Canada", "France", "Japan" };
            
            var MS2 = dataSource12.Union(dataSource22, StringComparer.OrdinalIgnoreCase).ToList();

            var QS2 = (from country in dataSource12 select country)
                                    .Union(dataSource22, StringComparer.OrdinalIgnoreCase).ToList();


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
            
            var MS3 = s1.Select(x => x.Name).Union(s2.Select(y => y.Name)).ToList();            
            var QS3 = (from std in s1 select std.Name).Union(s2.Select(y => y.Name)).ToList();

            var MS4 = s1.Select(x => x.Name).Union(s2.Select(y => y.Name), StringComparer.OrdinalIgnoreCase).ToList();
            var QS4 = (from std in s1 select std.Name).Union(s2.Select(y => y.Name), StringComparer.OrdinalIgnoreCase).ToList();

            //Now we need to select all the information of all the students from both the collections
            //by removing the duplicate students. 

            var MS5 = s1.Union(s2).ToList();            
            var QS5 = (from std in s1 select std).Union(s2).ToList();

            //it display all the students without removing the duplicate students. 
            //This is because the default comparer which is used for comparison is only checked 
            //whether two object references are equal and not the individual property values of 
            //the complex object.

        }
    }


    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }


}
