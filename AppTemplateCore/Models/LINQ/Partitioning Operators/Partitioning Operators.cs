using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ
{
    //What are Partitioning Operations in Linq?
    //The Partitioning Operations in Linq are used to divide a data source 
    //into two parts and then return one of them as output without changing the 
    //positions of the elements.

    //Why do we need Partitioning Operators?
    //We need to use Partitioning operators when we want to perform the following operations.


    //When you want to select the top n number of records from a data source.
    //If you want to select records from a data source until a specified condition is true.
    //Select records from a data source except for the first n number of records.
    //Skip records from a data source until a specified condition is true and then select all records.
    //It can be used to implement pagination for a data source.

    //Partitioning Methods Provided by Linq:
    //The following four methods are provided by LINQ to perform Partitioning Operations

    //Take
    //Skip
    //TakeWhile
    //SkipWhile


}
