using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Partitioning_Operators.Take
{

    //What is Take Operator and its need in C#.NET?
    //Examples using Take Operator.
    //Take Operator in LINQ using Filter.
    //What happens when Applying the Where Operator after the Take Operator?
    //Take Method on the Data Source which is null?


    //What is Take Operator and its need in C#.NET?
    //The Take Operator in Linq is used to fetch the first “n” number of elements from the data source
    //where “n” is an integer which is passed as a parameter to the Take method.
    //There is only one version available for this Take method as shown below.

    //the take method takes one integer count parameter and 
    //this method going to return that number of contiguous elements from the data source. 
    //If the data source is null then it is going to throw an exception.

    // it will not make any changes to the positions of the elements


    class Program22
    {
        static void Main22(string[] args)
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> ResultMS = numbers.Take(4).ToList();
            //Output: 1 2 3 4

           
            List<int> ResultQS = (from num in numbers select num).Take(4).ToList();
            //Output: 1 2 3 4

            //Take Method with Filtering Operator in C#.NET:
            //Applying Filtering before the Take method in C#:
            int[] ResultMS1 = numbers.Where(num => num > 3).Take(4).ToArray();
            //Output: 4 5 6 7
            List<int> ResultMS11 = numbers.Where(num => num > 3).Take(4).ToList();
            //Output: 4 5 6 7
            List<int> ResultQS1 = (from num in numbers where num > 3 select num).Take(4).ToList();
            //Output: 4 5 6 7

            //it is always a good programming practice to use the Where operator first and then the Take operator.
            //Applying Filtering after the Take method in C#:
            int[] ResultMS22 = numbers.Take(4).Where(num => num > 2).ToArray();            
            List<int> ResultQS22 = (from num in numbers select num).Take(4).Where(num => num > 2).ToList();
            //Output: 3 4


            //What happens when the Data Source is null?
            //When we are applying the Take operator on a data source which is null, 
            //then we will get an exception i.e.ArgumentNullException as shown in the below example.
            List<int> numbersbb = null;
            int[] ResultMSbb = numbersbb.Where(num => num > 3).Take(4).ToArray();


        }
    }


}
