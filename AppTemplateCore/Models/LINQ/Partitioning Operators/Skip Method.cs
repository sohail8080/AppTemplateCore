using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Partitioning_Operators
{
    //What is the Skip Method and its need in C#.NET?
    //Examples of Skip Method using both Method and Query Syntax.
    //Using the Skip Method with the Where Filtering Method.
    //What happens When we Applied the Filtering Operator after the Skip Operator in C#.NET?
    //Example when the Data source is null.





    class Program22
    {
        static void Main22(string[] args)
        {
            //What the Skip Method is and its need in C#.NET?
            //Skip Method is used to bypass the first “n” number of elements from a data source
            //and then returns the remaining elements from the data source as output.
            //Here “n” is an integer value passed to the Skip method as a parameter. 

            //Skip Method Using the Method syntax in LINQ:

            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> ResultMS = numbers.Skip(4).ToList();
            //Output: 5 6 7 8 9 10

            //Skip Method Using the Query Syntax in LINQ:

            List<int> numbers2 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> ResultQS2 = (from num in numbers2 select num).Skip(4).ToList();
            //Output: 5 6 7 8 9 10


            //Understanding the LINQ Skip Method with the Where Filtering Method:
            // first filter then skip
            List<int> numbers3 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            int[] ResultMS3 = numbers3.Where(num => num > 3).Skip(4).ToArray();           
            List<int> ResultQS3 = (from num in numbers3 where num > 3 select num).Skip(4).ToList();
            //Output: 8 9 10


            //Applying Filter Method after the Skip method in C#.NET:
            List<int> numbers4 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };           
            int[] ResultMS4 = numbers4.Skip(4).Where(num => num < 7).ToArray();           
            List<int> ResultQS4 = (from num in numbers4 select num).Skip(4).Where(num => num < 7).ToList();
            //Output: 5 6

            //What happens when the Data Source is null?
            //data source is null
            //we will get an exception i.e. ArgumentNullException 
            List<int> numbers5 = null;
            int[] ResultMS5 = numbers5.Skip(4).ToArray();


        }
    }



}
