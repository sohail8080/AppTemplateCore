using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.ElementOperators
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
           
            //Last Method in Linq:
            //The Last Method in Linq is used to return the last element from a data source.
            //There are two overloaded versions available for the Last method as shown below.

            //The second overloaded version of the Last method takes a predicate as a parameter. 
            //Using this predicate we can specify a condition and then it returns the last element 
            //which satisfied the specified condition. 

            //In this case, if no element satisfied the specified condition, 
            //then it will throw an exception.

            //Return the last element from a data source.

            int MethodSyntax43 = numbers.Last();

            //Return the last element from the data source which is less than 5.
            int MethodSyntax332 = numbers.Last(num => num < 5);

            // InvalidOperationException
            //If the data source is empty or if no element is satisfied with the given condition, 
            //then it will throw the InvalidOperationException as shown in the below example.

            //If you don’t want that Invalid Operation Exception, instead if you want a default value to be return based on the data type then you need to use the LastOrDefault method.

            //Note: the default is NULL for the reference types, and for the value types, 
            //the default value depends on the actual type expected.

            //LastOrDefault Method in Linq:
            //The LastOrDefault method exactly does the same thing as the Linq Last method except that 
            //the LastOrDefault method does not throw Invalid Operation Exception instead 
            //it returns the default value based on the data type of the element.

            //Fetch the last element from the data source using FirstOrDefault.
            int MethodSyntax4443 = numbers.LastOrDefault();

            //Fetch the last element from the data source which is less than 5.
            int MethodSyntax3332 = numbers.LastOrDefault(num => num < 5);

            int MethodSyntax244 = numbers.LastOrDefault(num => num > 50);

            // Last and LastOrDefault method Using Query Syntax

            int QuerySyntax1444 = (from num in numbers select num).Last();
            int QuerySyntax2444 = (from num in numbers select num).LastOrDefault();

            //What is the difference between Last and LastOrDefault methods in Linq?
            //Both Last and LastOrDefault methods in Linq are used to return the last element from a data source.
            //But if the data source is empty or if no element is satisfied with the specified condition, 
            //then the Last method will throw an exception while the LastOrDefault method 
            //will not throw an exception instead it returns a default value based on the 
            //data type of the element.

            
            
        }
    }



}
