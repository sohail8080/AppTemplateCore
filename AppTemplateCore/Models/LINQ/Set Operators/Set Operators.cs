using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.LINQ
{
    //Note: The most important thing that we always need to remember is, before learning something first we need to understand where and why we need to use that thing. With this keep in mind let’s proceed to this article.
    //That means these operations are performed either on a single data source or on multiple data sources and in the output some of the data are present and some of the data are absent. 

 
    //Except: We need to use the Except() LINQ Extension method when we want to return all the elements from the first data source which do not exists in the second data source. This method operates on two data sources.
    //Suppose we need to select all the Employees of a company except a particular department then you need to use Set Operations.

    //Intersect: This method is used to return the common elements from both the data sources i.e.the elements which exist in both the data set are going to returns as output.
    //Another example maybe if you have multiple classes and you want only to select all the toppers from all the classes then also you need to use Set Operations.

    //Union: This method is used to return all the elements which are present in either of the data sources.That means it combines the data from both the data sources and produce a single result set.
    //Suppose we have different data sources with similar structure and if we want to combine all the data sources into a single data source then we need to use Set Operations.







  




}
