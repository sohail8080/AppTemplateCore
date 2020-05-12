using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DB_Relationships
{
    //Entity Framework Core Fluent API

    //Fluent API in Entity Framework Core is a way to configure the model classes.
    //Fluent API uses the Modelbuilder instance to configure the domain model.
    //We can get the reference to the ModelBuilder, when we override the onmodelcreating method of the 
    //DbContext.The ModelBuilder has several methods, which you can use to configure the model. 
    //These methods are more flexible and provide developers with more power to configure the database 
    //than the EF Core conventions and data annotation attributes

    //What is Fluent API
    //A Fluent interface is a way of implementing an object-oriented API in a way that aims to provide 
    //for a more readable code Fluent interface resembles natural language making it easier to read 
    //and write.You can read about Fluent Interface from this link

    //Fluent Interface gives two distinct advantageous

    //Method chaining
    //More readable API Code
    //The ModelBuilder class uses the Fluent API to build the model.

    public class EFContext : DbContext
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EFCoreFluentAPI;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here   
        }
    }

    //ModelBuilder class includes important properties and methods to configure the domain model.

    //Fluent API Example in EF Core
    //Open Visual Studio 2019 and create C# -> .NET Core -> Console App (.NET Core). Name the project as EFCoreFluentAPI

    //Install the following packages


    //Install-package Microsoft.EntityFrameworkCore.SqlServer
    //Install-package Microsoft.EntityFrameworkcore.SqlServer.Design
    //Install-package Microsoft.EntityFrameworkcore.Tools

    //Create the models.cs under the root folder and add the employee class as shown below


    namespace EFCoreFluentAPI
    {
        public class Employee
        {
            public int EmployeeID { get; set; }
            public string Name { get; set; }
        }
    }


    namespace EFCoreFluentAPI
    {
        public class Employeedddddddddddddd
        {
            public int EmployeeID { get; set; }
            public string Name { get; set; }
        }
    }

    //Next, Create context class EFContext under the root folder



    namespace EFCoreFluentAPI
    {
        public class EFContexthhhh : DbContext
        {
            private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EFCoreFluentAPI;Trusted_Connection=True;";

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            }

            public DbSet<Employee> Employees { get; set; }
        }
    }

    //As mentioned earlier, we need to override the OnModelCreating method and use the instance of the 
    //ModelBuilder to configure the model

    //And then use the ModelBuilder instance.In the following code, we are invoking the HasDefaultSchema 
    //method to change the schema of the database to admin.

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.HasDefaultSchema("Admin");
    //    }

    //Next, we need to use the migrations to update/create the database as shown below

    //add-migration "v1"
    //update-database

    //You can see that the DbContext creates the Employee table under the Admin schema

    //Method chaining is one of the main features of EF Core Fluent API.For Example, the following code renames table as mstEmployee and defines EmployeeID as Primary Key

    //modelBuilder.Entity<Employee>()
    //            .ToTable("mstEmployee")
    //            .HasKey(e => e.EmployeeID);

    //modelBuilder.Entity<Employee>()
    //            .ToTable("mstEmployee")
    //            .HasKey(e => e.EmployeeID);

    //There are several methods available in EF Core Fluent API.These methods broadly classified into the three categories

    //Model wide configuration (database)
    //Entity Configuration (table)
    //Property configuration

    //Model-wide configuration
    //The Model wide properties are applied to the entire model.For Example, HasDefaultSchema defines the schema for every entity in the model.


    //modelBuilder.HasDefaultSchema("Admin");


    //modelBuilder.HasDefaultSchema("Admin");

    //The ModelBuilder class exposes several methods to configure the model.Some of the important methods are listed below


    //    METHOD DESCRIPTION
    //HasDefaultSchema Configures the default schema that database objects should be created in, if no schema is explicitly configured.
    //RegisterEntityType  Registers an entity type as part of the model
    //HasAnnotation Adds or updates an annotation on the model.If an annotation with the key specified in annotation already exists its value will be updated.
    //HasChangeTrackingStrategy Configures the default ChangeTrackingStrategy to be used for this model.This strategy indicates how the context detects changes to properties for an instance of an entity type.
    //Ignore Excludes the given entity type from the model.This method is typically used to remove types from the model that were added by convention.
    //HasDbFunction Configures a database function when targeting a relational database.
    //HasSequence Configures a database sequence when targeting a relational database.

    //    Entity Configuration
    //The configuration of the entity(Table) is done using the method Entity.The following code is an example of how to configure the Primary Key using the HasKey method.


    //modelBuilder.Entity().HasKey(e => e.EmployeeID);

    //modelBuilder.Entity().HasKey(e => e.EmployeeID);

    //    This method Entity returns the EntityTypeBuilder object to configure the entities.Some of the important methods available in the EntityTypeBuilder object are listed below


    //        METHOD DESCRIPTION
    //Ignore Exclude the entity from the Model.
    //ToTable Sets the table name for the entity type
    //HasKey  Sets the properties that make up the primary key for this entity type.
    //HasMany Configures a relationship where this entity type has a collection that contains instances of the other type in the relationship.e
    //HasOne  Configures a relationship where this entity type has a reference that points to a single instance of the other type in the relationship.
    //HasAlternateKey Adds or updates an annotation on the entity type. If an annotation with the key specified in annotation already exists its value will be updated
    //HasChangeTrackingStrategy Configures the ChangeTrackingStrategy to be used for this entity type. This strategy indicates how the context detects changes to properties for an instance of the entity type.
    //HasIndex Configures an index on the specified properties. If there is an existing index on the given set of properties, then the existing index will be returned for configuration.
    //OwnsOne Configures a relationship where the target entity is owned by (or part of) this entity. The target entity key value is always propagated from the entity it belongs to.

    //Ad by Valueimpression
    //Property Configuration
    //The EntityTypeBuilder object, which is explained above returns the Property Method. This method is used to configure the attributes of the property of the selected entity. The Property method returns the PropertyBuilder object, which is specific to the type being configured.

    //Fluent API in EF Core Property Configuration
    //METHOD DESCRIPTION
    //Ignore Exclude the Propery from the Model.
    //﻿HasColumnName Configures database column name of the property
    //HasColumnType Configures the database column data type of the property
    //HasDefaultValue Configures the default value for the column that the property maps to when targeting a relational database.
    //HasComputedColumnSql Configures the property to map to a computed column when targeting a relational database.
    //HasField Specifies the backing field to be used with a property.
    //HasMaxLength Specifies the maximum length of the property.
    //IsConcurrencyToken Enables the property to be used in an optimistic concurrency updates
    //IsFixedLength Configures the property to be fixed length. Use HasMaxLength to set the length that the property is fixed to.
    //IsMaxLength Configures the property to allow the maximum length supported by the database provider
    //IsReguired Specifies the database column as non - nullable.
    //IsUnicode Configures the property to support Unicode string content
    //ValueGeneratedNever Configures a property to never have a value generated when an instance of this entity type is saved.
    //ValueGeneratedOnAdd Configures a property to have a value generated only when saving a new entity, unless a non - null, non - temporary value has been set, in which case the set value will be saved instead.The value may be generated by a client-side value generator or may be generated by the database as part of saving the entity.
    //ValueGeneratedOnAddOrUpdate Configures a property to have a value generated when saving a new or existing entity.
    //ValueGeneratedOnUpdate Configures a property to have a value generated when saving an existing entity.




}
