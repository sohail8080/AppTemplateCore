using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.Prepend
{
    //Linq Prepend Method in C#:
    //The Linq Prepend Method is used to add one value to the beginning of a sequence.
    //This Prepend method like the Append method does not modify the elements of the sequence.
    //Instead, it creates a copy of the sequence with the new element.
    //return A new sequence that begins with the element.
    //When the source is null, it will throw ArgumentNullException.
    //This method is support from Framework 4.7.1 or later.

    class Program22
    {
        static void Main22(string[] args)
        {
            // Creating a list of numbers
            List<int> numberSequence = new List<int> { 10, 20, 30, 40 };


            // Trying to prepend 5
            // It will not work because the original sequence has not been changed
            numberSequence.Prepend(5);            
            Console.WriteLine(string.Join(", ", numberSequence));

            // It works now because we are using a changed copy of the original list
            Console.WriteLine(string.Join(", ", numberSequence.Prepend(50)));

            // If you prefer, you can create a new list explicitly
            List<int> newnumberSequence = numberSequence.Prepend(50).ToList();           
            Console.WriteLine(string.Join(", ", newnumberSequence));            
        }
    }



}
