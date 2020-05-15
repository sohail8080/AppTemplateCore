using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.ElementOperators.Firest
{

    //What are Element Operators in Linq?
    //The Element Operators in Linq are used to return a single element from a data source 
    //using the element index or based on a predicate i.e.a condition.
    //These Element Operators can be used with a single data source 
    //or on a query of multiple data sources.

    //Why do we need to use the Element Operators in Linq?
    //If you want to perform the following operations, then you need to use the Element Operators.

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
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //First Method in Linq: 
            //The Linq First Method is used to return the first element from a data source.
            //If the data source is empty, then this method will throw an exception.

            // Return the first element from a data source.

            int MethodSyntax5 = numbers.First();
            //Fetch the first element from the data source which is divisible by 2.
            int MethodSyntax6 = numbers.First(num => num % 2 == 0);

            //Example3: InvalidOperationException
            //Whenever the data source is empty or if the specified condition does not return any data, 
            //then we will get the InvalidOperationException as shown in the below example.

            //FirstOrDefault Method in Linq:
            //The Linq FirstOrDefault method exactly does the same thing as the First method except that 
            //this method does not throw Invalid Operation Exception instead it returns the default 
            //value based on the data type of the element.Like the First method, 
            //there are also two overloaded versions available for the FirstOrDefault method as shown below.

            int MethodSyntax666 = numbers.FirstOrDefault();

            //Fetch the element from the data source which is greater than 5.
            int MethodSyntax555 = numbers.FirstOrDefault(num => num > 5);

            //First and FirstOrDefault method Using Query Syntax

            int QuerySyntax166 = (from num in numbers select num).First();
            int QuerySyntax266 = (from num in numbers select num).FirstOrDefault();


            //What is the difference between First and FirstOrDefault Methods in Linq?
            //Both First and FirstOrDefault methods in Linq are used to 
            //return the first element from a data source.
            //But if the data source is empty or if the specified condition does not return any data, 
            //then the First method will throw an exception while the 
            //FirstOrDefault method will not throw an exception instead it returns 
            //a default value based on the data type of the element.

           
            
        }
    }



}
