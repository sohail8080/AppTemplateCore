using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DB_Relationships
{
    //Entity Framework Fluent API

    //In this Entity Framework Fluent API Tutorial, we will look at how to configure the model classes using Fluent API in C#. The Fluent API is implemented in the DBModelbuilder class

    //We have seen how to configure our database using Code First conventions.Code First Convention maps our POCO classes to the database by making assumptions based on how the domain classes are written.

    //The database can be further configured using the data annotations attributes.These Attributes are applied to the class or property.

    //The Fluent API is another way to configure our domain classes.It is more flexible and provides developers with more power to configure the database.

    //What is Fluent API
    //A Fluent interface is a way of implementing an object-oriented API in a way that aims to provide for more readable code Fluent interface resembles natural language making it easier to read and write.You can read about Fluent Interface from this link

    //Entity Framework Fluent API uses the Fluent Interface.Fluent API is implemented in DBModelbuilder class.

//    DBModlebuilder Class
//The Fluent API uses the DbModelbuilder class to map domain classes to a database schema.The reference to the DBModlebuilder class is obtained by overriding the OnModelCreating method of the DbContext object

//OnModelCreating
//This method is called before DBContext initializes the model.The initialization does not happen when the DBContext created. It happens when the Context is used for the first time.During the initialization, DBContext creates the DbModelBuilder instance.It then initializes all the domain classes.Then it calls the OnModelCreating Method and passes the reference to the DbModelBuilder

//The OnModelCreating method can be overridden in our code as shown in the example below.We can then, get the reference to the DBModelbuilder in our overridden class. We can use DBModelBuilder along with the fluent API to configure our model.


//protected override void OnModelCreating(DbModelBuilder modelBuilder)
//    {
//        //Configure domain classes using modelBuilder here
//        base.OnModelCreating(modelBuilder);
//    }



//protected override void OnModelCreating(DbModelBuilder modelBuilder)
//    {
//        //Configure domain classes using modelBuilder here
//        base.OnModelCreating(modelBuilder);
//    }


//    Fluent API Example Project
//Software versions used in this tutorial.

//Visual Studio 2015
//Entity framework 6.2.0.
//.NET 4.5
//C#

//Open Visual Studio and go to File -> New -> Project.Select Template Visual C# -> Windows -> Console Application. Name the project as EFFluentAPI. Click on OK to create the Project

//Install the entity framework by running the following command install-package entityframework from the NuGet Package Manager

//Right click on the project and create employee.cs and add the following employee model


//namespace EFFluentAPI
//    {
//        public class Employee
//        {
//            public int EmployeeID { get; set; }
//            public string Name { get; set; }
//        }
//    }

//1
//2
//3
//4
//5
//6
//7
//8
//9
//10
 
//namespace EFFluentAPI
//    {
//        public class Employee
//        {
//            public int EmployeeID { get; set; }
//            public string Name { get; set; }
//        }
//    }

//    Next, create the context class EFContex in the root of the project



//using System.Data.Entity;

//namespace EFFluentAPI
//    {
//        public class EFContext : DbContext
//        {
//            public EFContext() : base("EF6FluentAPI")
//            {
//                Database.SetInitializer<EFContext>(new DropCreateDatabaseAlways<EFContext>());
//            }


//            public DbSet<Employee> Employees { get; set; }
//        }
//    }

//1
//2
//3
//4
//5
//6
//7
//8
//9
//10
//11
//12
//13
//14
//15
//16
//17
//18
 
 
//using System.Data.Entity;
 
//namespace EFFluentAPI
//    {
//        public class EFContext : DbContext
//        {
//            public EFContext() : base("EF6FluentAPI")
//            {
//                Database.SetInitializer<EFContext>(new DropCreateDatabaseAlways<EFContext>());
//            }


//            public DbSet<Employee> Employees { get; set; }
//        }
//    }


//    Ad by Valueimpression
//    Finally, update the Main method in the program.cs


//using System;
//using System.Linq;


//namespace EFFluentAPI
//    {
//        class Program
//        {
//            static void Main(string[] args)
//            {
//                try
//                {
//                    using (var ctx = new EFContext())
//                    {
//                        ctx.Employees.Select(e => e.EmployeeID == 0);
//                    }
//                }

//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.Message);
//                }

//                Console.WriteLine("Press any key to close");
//                Console.ReadKey();
//            }
//        }
//    }

 
//using System;
//using System.Linq;
 
 
//namespace EFFluentAPI
//    {
//        class Program
//        {
//            static void Main(string[] args)
//            {
//                try
//                {
//                    using (var ctx = new EFContext())
//                    {
//                        ctx.Employees.Select(e => e.EmployeeID == 0);
//                    }
//                }

//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.Message);
//                }

//                Console.WriteLine("Press any key to close");
//                Console.ReadKey();
//            }
//        }
//    }

//    Run the above code.It will generate the database EF6FlunetAPI with table Employee
    

//    The default convention in EF is to create the table under the schema dbo
    

//    Fluent API Default Convention
//    Now let us change that using the Fluent API

//Using Fluent API to change the Schema
//As mentioned above we need to need to override the OnModelCreating method of the dbcontext class. This gives us the instance of the DbModelBuilder object.


//Ad by Valueimpression
//HasDefaultSchema
//Then, You can change the schema of the database using the HasDefaultSchema method of the dbModelbuilder object.

//The Code is as shown below.Copy this code to the EFContext class


//protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.HasDefaultSchema("Admin");
//    }


//protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.HasDefaultSchema("Admin");
//    }

//    The above method creates the table employee with the schema “Admin” as shown in the image below.

//    Fluent API Example Application
//The above example demonstrates you how to use Fluent API using a small example.


//Ad by Valueimpression
//DBModelBuilder
//The DbModelBuilder class includes several methods to configure the domain model.These methods are classified as follows.

//Model wide configuration(database)
//Entity Configuration(table)
//Property configuration

//Ad by Valueimpression
//Model wise configuration
//Model-wide properties are applied to the entire model.The DbModelBuilder class exposes the following properties/methods to configure the model.Some of the important properties and methods are listed below

//METHOD DESCRIPTION
//HasDefaultSchema Configures the default database schema name to be used for the model.This can be overridden at the entity level
//RegisterEntityType  Registers an entity type as part of the model


//Ad by Valueimpression
//Entity Configuration
//The configuration of the entity is done using the method Entity<TEntityType>.This method returns the entitytypeconfiguration object to configure the entities.Some of the important methods available in entitytypeconfiguration object are listed below


//Entity Type Mapping
//Entity Type Mapping
//METHOD DESCRIPTION
//ToTable Sets the table name for the entity type
//HasKey  Configures the primary key for the selected entity type
//HasMany Configures many relationships for the selected entity type
//HasOptional Configures an optional relationship for the selected entity type. Allows you save the Instances of the entity type to the database without this relationship being specified.This creates the nullable foreign key in the database
//HasRequired Configures a required relationship for the selected entity type. Does not allow you to save the Instances of the entity type without the relationship being specified.This creates not null foreign key in the database
//You can refer to Configure the Entity mappings Using Fluent API to find out how to use these methods


//Ad by Valueimpression
//Property Configuration
//The EntityTypeConfiguration object, which is explained above returns the Property Method. This method is used to configure the attributes of the property of the selected entity.


//The Property method returns the configuration object, which is specific to the type being configured. For Example, calling property method on the type String property will return StringPropertyConfiguration object. Similarly, property method returns DecimalPropertyConfiguration, BinaryPropertyConfiguration, DateTimePropertyConfiguration, PrimitivePropertyConfiguration is returned by the Property method depending on the type of the Property


//Each of these PropertyConfiguration objects exposes methods to configure the Properties.

//Entity Type Property Configuration
//Entity Type Property Configuration
//METHOD DESCRIPTION

//HasColumnName Configures database column name of the property
//HasColumnOrder Configures the order in which the column appears in the database table.It is also used while creating the composite primary key to specify the key order.
//HasColumnType Configures the database column data type of the property
//HasDatabaseGeneratedType Configures how values for the property are generated by the database
//HasMaxLength    Specifies the maximum length of the property.
//HasParameterName Specifies the name of the parameter used in stored procedures for this property.
//IsConcurrencyToken Enables the property to be used in an optimistic concurrency updates
//IsFixedLength Configures the property to be fixed length.Use HasMaxLength to set the length that the property is fixed to.
//IsMaxLength Configures the property to allow the maximum length supported by the database provider
//IsOptional Specifies the database column as nullable
//IsReguired  Specifies the database column as non-nullable.
//IsUnicode Configures the property to support Unicode string content
//IsVariableLength Configures the property to be variable length


//Ad by Valueimpression
//You can read How to Configure Property mappings using Fluent API to know more about how to use these methods.

//In the next tutorials, we will show how to use Fluent API to configure the model, entity Type (Table), Property (Column) and relationships


























}
