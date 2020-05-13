using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ.JoinOperators
{

    //If you have any experience in database systems like SQL Server, Oracle, MySQL, etc. 
    //then you may have familiar with SQL Joins. The LINQ Joins are not different. 
    //They are also used to merge the data from two or more data sources (tables or objects) 
    //into a single result set based on some common property. 
    //Using LINQ Joins, it is also possible to fetch the data based on some conditions.

    //What are Linq Join Operations?
    //As per the Microsoft documentation “A join of two data sources is the association of objects 
    //in one data source with objects that share a common attribute in another data source”.

    //We can simplify the above definition as 
    //“Join operations are used to fetch the data from two or more data sources based on 
    //some common properties present in the data sources”.

    //Why do we need to perform Join Operations?
    //we have the following three data sources (Employee, Department, and Address).

    //As you can see the above result set contains the data from all the three data sources.
    // while performing join you need the common property.
    //common property between Employee and Department is Department Id 
    //which is present in both the data sources. 
    //In the same line, Address ID is the common property between the Employee and Address data sources.
    //So scenarios like this we need to use Linq Join to fetch the data. 
    //How to write the query to fetch the data we will discuss in our next article.

    //What are the Methods available in Linq to Perform the Join Operations?
    //There are two methods available in Linq to perform Join Operations.They are as follows:


    //Join: This operator is used to join two data sources or collection based on common property and 
    //return the data as a single result set.

    //GroupJoin: This operator is also used to join two data sources or collections based on a common key or property but return the result as a group of sequences.
    //What are the different types of Linq Joins available in C#?
    //We can perform different types of joins such as 
    //Inner Join, Left Join, Right Join, Full Join, and Cross Join in Linq.

}
