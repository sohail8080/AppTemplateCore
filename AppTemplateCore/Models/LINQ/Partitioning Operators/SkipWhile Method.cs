using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Partitioning_Operators.SKIPWHILE
{

    //What the SkipWhile Method is and its need in C#.NET?
    //Examples of SkipWhille Method using both Method and Query Syntax.
    //Example using Index.

    // SKIP WHILE THE CONDIOTION IS TRUE, AS CONDITION IS FALSE RETURN THE RAMAINING
    //The SkipWhile Method in Linq is used to skip all the elements from a data source
    //as long as the given the condition specified by the predicate is true and 
    //then returns the remaining element from the sequence as an output.

    //once the condition is failed then the SkipWhile method will not check the rest of the elements 
    //even though the condition is true for some of the remaining elements.


    class Program22
    {
        static void Main22(string[] args)
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> ResultMS = numbers.SkipWhile(num => num < 5).ToList();
            //Output: 5 6 7 8 9 10

            List<int> numbers2 = new List<int>() { 1, 4, 5, 6, 7, 8, 9, 10, 2, 3 };
            List<int> ResultMS2 = numbers2.SkipWhile(num => num < 5).ToList();
            //Output: 5 6 7 8 9 10 2 3

            List<string> names = new List<string>() { "Pam", "Rahul", "Kim", "Sara", "Priyanka" };
            List<string> namesResult = names.SkipWhile(name => name.Length < 4).ToList();
            //Output: Rahul Kim Sara Priyanka

            //Example4: Using Index
            List<string> names2 = new List<string>() { "Sara", "Rahul", "John", "Pam", "Priyanka" };
            List<string> namesResult2 = names2.SkipWhile((name, index) => name.Length > index).ToList();
            //Output: Pam Priyanka

        }
    }


}
