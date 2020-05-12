using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Intersect.SingValue
{
    //The LINQ Intersect Method in C# is used to return the common elements from both the collections. 
    //The elements that are present in both the data sources. 


    class Program
    {
        static void Main(string[] args)
        {
            //Note: In query syntax, there is no such operator call Intersect, so here we need to use 
            //the mixed syntax i.e. both the query and method syntax to achieve the same.

            List<int> dataSource1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            List<int> dataSource2 = new List<int>() { 1, 3, 5, 8, 9, 10 };
            var MS = dataSource1.Intersect(dataSource2).ToList();           
            var QS = (from num in dataSource1 select num).Intersect(dataSource2).ToList();


            string[] dataSource11 = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] dataSource21 = { "India", "uk", "Canada", "France", "Japan" };            
            var MS1 = dataSource11.Intersect(dataSource21).ToList();            
            var QS1 = (from country in dataSource11 select country).Intersect(dataSource21).ToList();


            string[] dataSource12 = { "India", "USA", "UK", "Canada", "Srilanka" };
            string[] dataSource22 = { "India", "uk", "Canada", "France", "Japan" };           
            var MS2 = dataSource12.Intersect(dataSource22, StringComparer.OrdinalIgnoreCase).ToList();            
            var QS2 = (from country in dataSource12 select country)
                      .Intersect(dataSource22, StringComparer.OrdinalIgnoreCase).ToList();

            //LINQ Intersect() Method with Complex Type:


            List<Student> Stud1 = new List<Student>()
            {
                new Student {ID = 101, Name = "Preety" },
                new Student {ID = 102, Name = "Sambit" },
                new Student {ID = 105, Name = "Hina"},
                new Student {ID = 106, Name = "Anurag"},
            };

            List<Student> Stud2 = new List<Student>()
            {
                new Student {ID = 105, Name = "Hina"},
                new Student {ID = 106, Name = "Anurag"},
                new Student {ID = 107, Name = "Pranaya"},
                new Student {ID = 108, Name = "Santosh"},
            };

           
            var MS3 = Stud1.Select(x => x.Name).Intersect(Stud2.Select(y => y.Name)).ToList();
           
            var QS3 = (from std in Stud1 select std.Name).Intersect(Stud2.Select(y => y.Name)).ToList();



            var MS34 = Stud1.Select(x => x.Name).Intersect(Stud2.Select(y => y.Name), StringComparer.OrdinalIgnoreCase).ToList();

            var QS34 = (from std in Stud1 select std.Name)
                      .Intersect(Stud2.Select(y => y.Name), StringComparer.OrdinalIgnoreCase).ToList();


            //Now we need to select all the information of all the students those are present
            //in both the collections.In order to do this, let us modify the 
            //program class as shown below.
            
            var MS5 = Stud1.Intersect(Stud2).ToList();

            var QS5 = (from std in Stud1 select std).Intersect(Stud2).ToList();

            //Once you run the application, then it will not display any data. 
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
