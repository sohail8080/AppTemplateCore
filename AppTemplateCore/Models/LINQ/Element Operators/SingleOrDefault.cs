using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.ElementOperators.Single
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
            
            //Single Method in Linq: 
            //The Linq Single Method is used to returns a single element from a data source
            //There are two overloaded versions available for this Linq Single Method, 
            //which are shown in the below image.

            //As you can see, the first overloaded version of the Single method does not take any parameter. 
            //This method simply returns the only element from a sequence. 
            //If the data source is empty or if the data source contains more than one element, 
            //then it throws an exception.

            //On the other hand, the second overloaded version of the Single method 
            //takes one predicate as a parameter and using this predicate you can specify a condition. 
            //This method returns the only element from the sequence which satisfied the given condition. 
            //In this case, the method will throw an exception when any of the following condition is true.

            //If the data source is empty.
            //When the given condition does not satisfy any element in the sequence.
            //If the given condition satisfies more than one element.

            List<int> numbers22 = new List<int>() { 10 };
            int number22 = numbers22.Single();//Output: 10

            //Using Single Method with Empty Data source
            //throw an exception : Invalid Operation Exception
            List<int> numbers33 = new List<int>() { };
            int number33 = numbers33.Single();

            //Sequence contains more than one element
            //throw an exception : Invalid Operation Exception
            List<int> numbers44 = new List<int>() { 10, 20, 30 };
            int number44 = numbers44.Single();

            //If your sequence contains more than one element and you need to fetch a single element 
            //based on some condition then you need to use the other overloaded versions of the 
            //Single method which takes one predicate as a parameter.
            //Using that predicate we specify our condition which is going to return only one element from the sequence.

            List<int> numbers55 = new List<int>() { 10, 20, 30 };
            int number55 = numbers55.Single(num => num == 20);//Output: 20

            //specified condition returns more than one element and hence we will get an exception saying the
            //sequence contains more than one matching element.
            List<int> numbers66 = new List<int>() { 10, 20, 30 };
            int number66 = numbers66.Single(num => num > 10);

            //specified Single method does not return any data and hence we will get sequence contains 
            //no matching element exception.
            List<int> numbers77 = new List<int>() { 10, 20, 30 };
            int number77 = numbers77.Single(num => num < 10);

            //If you don’t want to throw an exception when the 
            //sequence is empty or 
            //if the specified condition does not return an element from a sequence then 
            //you need to use the Linq SingleOrDefault method.
            // If the sequence is empty then it returns the default value without throwing an exception.
            //SingleOrDefault method still throws an exception when the 
            //sequence contains more than one matching element for the given condition.

            //SingleOrDefault method will return the “0” as “0” is the default value for the integer data type.
            List<int> numberscc = new List<int>() { 10, 20, 30 };
            int numbercc = numberscc.SingleOrDefault(num => num < 10);

            //we will get the default value 0.
            List<int> numbersaa = new List<int>() { };
            int numberaa = numbersaa.SingleOrDefault(num => num < 10);

            //we will get an exception. This is because now the sequence contains more than elements 
            //for the specified condition.
            List<int> numbersbb = new List<int>() { 10, 20, 30 };
            int numberbb = numbersbb.SingleOrDefault(num => num > 10);

            //What is the difference between Single and SingleOrDefault methods in Linq?
            //Both Single and SingleOrDefault methods in Linq are used to returns a single element from a sequence.
            //But if the sequence is empty or if no element is satisfied with the given condition, 
            //then the Single method will throw an exception while the 
            //SingleOrDefault method will not throw an exception instead it returns a default value.
            
        }
    }



}
