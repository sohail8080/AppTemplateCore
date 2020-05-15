using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.ElementOperators.ElementAt
{

    //What are Element Operators in Linq?
    //The Element Operators in Linq are used to return a single element from a data source 
    //using the element index or based on a predicate i.e.a condition.
    //These Element Operators can be used with a single data source 
    //or on a query of multiple data sources.

    //Why do we need to use the Element Operators in Linq?
    //If you want to perform the following operations, 
    //then you need to use the Element Operators.

    //Select the First record from a data source.
    //Fetch a specific record from the data source.
    //Select the last record from a data source.

    //What methods are available in the Element Operators category?
    //The following methods are provided by Linq to perform element operations.

    //ElementAt and ElementAtOrDefault
    //First and FirstOrDefault
    //Last and LastOrDefault
    //Single and SingleOrDefault
    //DefaultIfEmpty



    class Program2
    {
        static void Main2(string[] args)
        {

            //ElementAt Operator in Linq: 
            //The ElementAt operator is used to return an element from a specific index. 
            //If the data source is empty or if the provided index value is out of range, 
            //then we will get ArgumentOutOfRangeException.

            //As you can see, this method takes one parameter i.e. the index position. 
            //Then it will return the element present in that index position of the data source.

            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int MethodSyntax = numbers.ElementAt(1);
            int QuerySyntax1 = (from num in numbers select num).ElementAt(1);

            // Index Out of Range
            //Empty Data source
            //If you don’t want that exception then you need to use the ElementAtOrDefault method.

            //ElementAtOrDefault Method in Linq:
            //The ElementAtOrDefault method does the same thing as the ElementAt method except that this 
            //method does not throw an exception when the data source is empty or when the supplied index value 
            //is out of range. In such cases, it will return the default value based on the data type of 
            //the element the data source contain.

            int MethodSyntax2 = numbers.ElementAtOrDefault(1);
            int MethodSyntax3 = numbers.ElementAtOrDefault(10);//Output: 0
            int QuerySyntax2 = (from num in numbers select num).ElementAtOrDefault(1);

            //What is the difference between the ElementAt and ElementAtOrDefault method?
            //Both methods are used to return an element from the specified index.
            //But if the element is not available at the specified index position, 
            //then the ElementAt method will throw an exception while the ElementAtOrDefault method 
            //will not throw an exception instead it returns a default value based on the data type of 
            //the element.

            
            
        }
    }



}
