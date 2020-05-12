using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.QuantifierOperation
{
    //What are the LINQ Quantifier Operations?
    //We need to use the LINQ Quantifier Operators on a data source when we want to 
    //check if some or all  of the elements of that data source satisfy a condition or not.

    //That means, here we have a data source and also we have a condition.
    //Then we need to check whether 
    //all or some of the elements of that data source satisfied the condition or not.

    //All the methods in quantifier operations are always going to return a Boolean value.
    //That means if the all or some of the elements in the data source satisfy the given condition 
    //then it is going to return true else it is going to return false.

    //Note: The condition that we specify may be for some or all of the elements.

    //Examples:
    //We need to use the quantifier operations on the below scenarios.

    //When we want to check whether all the employees are present on a specific day or not
    //In a specific class of students, we need to check if any of the students having marks more than 90%
    //When we need to check if any of the employees having the names James is present or not
    //What methods are available in this category?
    //The following three methods belong to the Quantifier Operations category.

    //All: This specifies whether all the elements of a data source satisfy a given condition or not.
    // ALL ELEMENTS IN COLLECTION SATISFY THE CONDITION.

    //Any: This specifies whether at least one of the elements of a data source satisfies the condition or not.
    // ANY ONE ELEMENT IN COLLECTION SPECIFY THE CONDITION

    //Contains: This method is used to check whether the data source contains a specified element or not.
    // COLLECTION CONTAINS SPECIFIC ELMENT OR NOT.

    //Note: All of the above three methods return Boolean true or false depending on 
    //whether all or some of the elements in a data source satisfy a condition.


    class Program
    {
        static void Main(string[] args)
        {
            int[] IntArray = { 11, 22, 33, 44, 55 };
            var Result = IntArray.All(x => x > 10);
            Console.WriteLine("Is All Numbers are greater than 10 : " + Result);
            Console.ReadKey();
        }
    }



}
