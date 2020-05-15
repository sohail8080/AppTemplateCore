using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Partitioning_Operators.TakeWhile
{
    //What are the need and use of the TakeWhile Method in C#.NET?
    //Examples using both Method and Query Syntax.
    //Understanding TakeWhile Operator with Filtering Operator.
    //What is the difference between where and TakeWhile method in Linq?
    //Different Types of examples using the TakeWhile Operator in Linq.

    //What are the need and use of the TakeWhile Method in C#.NET?
    //The TakeWhile Method in Linq is used to fetch all the elements from a data source until 
    //a specified condition is true. 
    //Once the condition is failed, then the TakeWhile method will not check the rest of the elements 
    //presents in the data source even though the condition is true for the remaining elements.

    //The TakeWhile method will not make any changes to the positions of the elements.
    //There are two overloaded versions available for this method as shown in the below image.

    //The First version of the TakeWhile method returns elements from a sequence as long as the specified condition is true.

    //The second overloaded version of this method returns elements from a sequence as long as the given
    //condition is true.

    //The second parameter of the function represents the index of the source elements.
    //The element’s index can be used in the logic of the predicate function.

    class Program22
    {
        static void Main22(string[] args)
        {

            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> ResultMS = numbers.TakeWhile(num => num < 6).ToList();
            //Output: 1 2 3 4 5

            //TakeWhile Method along with the Where Filtering Method in C#.NET.
            //Now you may have one question in your mind, that the same thing can be achieved using 
            //the Linq Where extension method as shown below.

            List<int> ResultMS1 = numbers.Where(num => num < 6).ToList();
            //Output: 1 2 3 4 5


            List<int> numbers22 = new List<int>() { 1, 2, 3, 6, 7, 8, 9, 10, 4, 5 };
            List<int> Result1 = numbers22.TakeWhile(num => num < 6).ToList();
            //1, 2, and 3             
            List<int> Result2 = numbers.Where(num => num < 6).ToList();
            //1, 2, 3, 4, and 5


            List<string> names = new List<string>() { "Sara", "Rahul", "John", "Pam", "Priyanka" };
            List<string> namesResult = names.TakeWhile(name => name.Length > 3).ToList();
            //Output: Sara Rahul John

            //Let us use the other overloaded version which takes an index as a parameter. 
            //Here, in the following example, we will check the length of the name should be greater than 
            //its index position.
            List<string> names2 = new List<string>() { "Sara", "Rahul", "John", "Pam", "Priyanka" };
            List<string> namesResult2 = names2.TakeWhile((name, index) => name.Length > index).ToList();
            //Output: Sara Rahul John




        }
    }

}
