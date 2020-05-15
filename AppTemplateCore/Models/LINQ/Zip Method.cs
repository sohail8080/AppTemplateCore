using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Zip
{

    //Zip Method:
    //The Linq Zip Method in C# is used to apply a specified function to the corresponding elements 
    //of two sequences and producing a sequence of the results. 

    //Parameters:
    //IEnumerable<TFirst> first: The first sequence to merge.
    //IEnumerable<TSecond> second: The second sequence to merge.
    //Func<TFirst, TSecond, TResult> resultSelector: A function that specifies how to merge the elements from the 
    //two sequences.

    //Returns: This method is going to return an IEnumerable<T> that contains 
    //merged elements of two input sequences.

    //Exceptions: This method is going to throw ArgumentNullException when 
    //either the first or the second input sequence is null.

    //Note: The Zip method merges each element of the first sequence with an element in the second sequence 
    //that has the same index position.
    //If both the sequences do not have the same number of elements, 
    //then the Zip method merges sequences until it reaches the end of the sequence 
    //which contains fewer elements. 

    //For example, if one sequence has five elements and the other sequence has four elements, 
    //then the result sequence will have only four elements.

    class Program22
    {
        static void Main22(string[] args)
        {
            int[] numbersSequence = { 10, 20, 30, 40, 50 };

            string[] wordsSequence = { "Ten", "Twenty", "Thirty", "Fourty" };

            var resultSequence = numbersSequence.Zip(wordsSequence, (first, second) => first + " - " + second);

            foreach (var item in resultSequence)
            {
                Console.WriteLine(item);
            }
            // 10, 20, 30, 40 ,,,, 50 will be skipped

            //The first sequence contains 5 elements whereas the second sequence contains 4 elements. 
            //So, for the fifth element of the first sequence, there is no corresponding fifth element in 
            //the second sequence. 
            //As a result, the Zip method merges the four elements and that’s what we have seen in the output.

            //The Zip method is implemented by using the deferred execution. 
            //So, the immediate return value of this method is going to be an object which stores all 
            //the required information which is required to perform the action. 
            //The query represented by this method is not executed until the object is enumerated 
            //either by calling its GetEnumerator method directly or by using for each loop.

        }
    }

}
