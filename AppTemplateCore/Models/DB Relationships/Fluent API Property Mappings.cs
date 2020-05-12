using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DB_Relationships
{

    //    Property Mappings Using Fluent Api

    //In this Tutorial let us look at how to configure the model properties and their related database columns using the fluent API.Using Fluent API you can change the ColumnName using HasColumnName method.Alter the Order in which Column Appears Using HasColumnOrder.Set the Colum DataType using HasColumnType,  Make Column as Nullable Using IsOptional,  Set it as Not Null using isRequired. Set the Column Length using HasMaxLength, isMaxLength, IsFixedLength.Control the Precision of Decimal Columns using HasPrecision.  You can also configure the auto-generated values using the HasDatabaseGeneratedOption.In this tutorial, we will look at all these methods in detail



    //    Property Mappings Using Fluent API
    //In the last tutorial, we used entitytypeconfiguration (Entity<TEntityType>) configure, the entity types.This object has a Property method.The Property method is used to configure attributes of the property of the entity.

    //The Property method returns the configuration object. The returned Configuration object is specific to the type being configured. For Example date property returns DateTimePropertyConfiguration object. Similarly StringPropertyConfiguration, DecimalPropertyConfiguration, BinaryPropertyConfiguration, PrimitivePropertyConfiguration are returned by the Property method depending on the data type


    //Ad by Valueimpression
    //Entity Type Property Configuration
    //Entity Type Property Configuration
    //The Configuration object has several methods, which can be used to configure the Property as shown below.

    //For this example we will use the following Model class. The model class has Employee , department and Project domain models


    // public class Employee
    //    {
    //        public int EmployeeID { get; set; }
    //        public string Name { get; set; }
    //        public DateTime DOB { get; set; }
    //        public string Address { get; set; }
    //        public string Email { get; set; }
    //        public string Remark { get; set; }
    //        public byte[] RowID { get; set; }
    //    }
    //    public class Department
    //    {
    //        public int DepartmentID { get; set; }
    //        public string Name { get; set; }
    //        public string Manager { get; set; }
    //    }
    //    public class Project
    //    {
    //        public int ProjectID { get; set; }
    //        public string Name { get; set; }
    //        public DateTime StartDate { get; set; }
    //        public bool Active { get; set; }
    //        public decimal Budget { get; set; }
    //        public int NoOfEmployee { get; set; }
    //    }



    // public class Employee
    //    {
    //        public int EmployeeID { get; set; }
    //        public string Name { get; set; }
    //        public DateTime DOB { get; set; }
    //        public string Address { get; set; }
    //        public string Email { get; set; }
    //        public string Remark { get; set; }
    //        public byte[] RowID { get; set; }
    //    }
    //    public class Department
    //    {
    //        public int DepartmentID { get; set; }
    //        public string Name { get; set; }
    //        public string Manager { get; set; }
    //    }
    //    public class Project
    //    {
    //        public int ProjectID { get; set; }
    //        public string Name { get; set; }
    //        public DateTime StartDate { get; set; }
    //        public bool Active { get; set; }
    //        public decimal Budget { get; set; }
    //        public int NoOfEmployee { get; set; }
    //    }

    //    Download the source code from GitHub

    //HasColumnName
    //This method is used to set the database column name.The following code sets the name of the column as employeeName for the property name.


    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Name)
    // .HasColumnName("EmployeeName")



    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Name)
    // .HasColumnName("EmployeeName")


    //HasColumnOrder
    //This method is used to set the order which column appears in the table.The following code Name property will appear first in the table.


    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Name)
    // .HasColumnOrder(1);



    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Name)
    // .HasColumnOrder(1);

    //    HasColumnType
    //It is used to set the column Data Type of the property.By Convention Code First creates nvarchar(max) for string, int for integer, varbinary(max) for Byte array, bit for boolean etc. You can read about Code First Conventions from here

    //You can override the default behaviour using the HasColumnType method


    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Name)
    // .HasColumnType("varchar");



    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Name)
    // .HasColumnType("varchar");


    //    modelBuilder.Entity<Employee>()
    // .Property(p => p.DOB)
    // .HasColumnType("Date");



    //modelBuilder.Entity<Employee>()
    // .Property(p => p.DOB)
    // .HasColumnType("Date");

    //    IsOptional
    //IsOptional is used to configures the Property to Null.The following creates a nullable column for address property.


    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Address)
    // .IsOptional();



    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Address)
    // .IsOptional();


    //    Ad by Valueimpression
    //    IsRequired
    //IsRequired method used to Configure the Property to Not Null.The following code creates a Non Nullable column for email property.


    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Email)
    // .IsOptional();



    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Email)
    // .IsOptional();

    //    HasMaxLength
    //    This method sets the maximum length of the property.In the following code Address column is created by EF with the length 100



    //    modelBuilder.Entity<Employee>()
    //     .Property(p => p.Address)
    // .HasMaxLength(100);



    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Address)
    // .HasMaxLength(100);

    //    IsMaxLength
    //    Configures the property to maximum length allowed by the database.In the following code Column Remark is mapped to nvarchar(max) data type


    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Remark)
    // .IsMaxLength();



    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Remark)
    // .IsMaxLength();

    //    IsFixedLength
    //    Configures the property to be fixed Length.The Fixed Length must be specified using the HasMaxLength method.This method converts the nvarchar to nchar column type



    //   modelBuilder.Entity<Department>()
    //    .Property(p => p.Manager)
    // .HasMaxLength(50)
    // .IsFixedLength();



    //modelBuilder.Entity<Department>()
    // .Property(p => p.Manager)
    // .HasMaxLength(50)
    // .IsFixedLength();


    //    Ad by Valueimpression
    //    Ignore
    //Use this method to disable mapping of the property to the database column.In the following code, NoOfEmployee column will not be created


    //modelBuilder.Entity<Project>()
    // .Ignore(e => e.NoOfEmployee);



    //modelBuilder.Entity<Project>()
    // .Ignore(e => e.NoOfEmployee);

    //    HasPrecision
    //This method is used to configure the Precision and scale of Decimal properties.


    //modelBuilder.Entity<Project>()
    // .Property(p => p.Budget)
    // .HasPrecision(10, 2);



    //modelBuilder.Entity<Project>()
    // .Property(p => p.Budget)
    // .HasPrecision(10, 2);

    //    IsConcurrencyToken
    //This method applied to a property which you want to participate in concurrency check


    //modelBuilder.Entity<Employee>()
    // .Property(t => t.Name)
    // .IsConcurrencyToken();



    //modelBuilder.Entity<Employee>()
    // .Property(t => t.Name)
    // .IsConcurrencyToken();

    //    IsRowVersion
    //This method used to create the column with datatype rowversion. rowversion columns are used in concurrency check. This method can only be applied on byte[]
    //    array properties


    //modelBuilder.Entity<Employee>()
    // .Property(t => t.RowID)
    // .IsRowVersion();


    //modelBuilder.Entity<Employee>()
    // .Property(t => t.RowID)
    // .IsRowVersion();

    //    Unicode
    //This method is used to specify that the string should be of Unicode or not.


    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Remark)
    // .IsUnicode(true);

    //1
    //2
    //3
    //4
    //5

    //modelBuilder.Entity<Employee>()
    // .Property(p => p.Remark)
    // .IsUnicode(true);


    //    Ad by Valueimpression
    //HasDatabaseGeneratedOption
    //This method used to configure the database generated values. It has three options

    //DatabaseGeneratedOption.Identity
    //The identity values are generated when the row is inserted.
    //DatabaseGeneratedOption.Computed
    //The value is generated when the data is inserted or updated to the database
    //DatabaseGeneratedOption.None
    //The value of the property is not generated by the database when the row is inserted or updated
    //The following code below disables the auto-generated identity values for the Primary key column ProjectID


    //modelBuilder.Entity<Project>()
    // .Property(p => p.ProjectID)
    // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

    //1
    //2
    //3
    //4
    //5

    //modelBuilder.Entity<Project>()
    // .Property(p => p.ProjectID)
    // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

    //    HasParameterName
    //This method sets the name of the parameter to be used for this property in stored procedures


    //modelBuilder.Entity<Project>()
    // .Property(p => p.Name)
    // .HasParameterName("ProjectName");

    //1
    //2
    //3
    //4
    //5

    //modelBuilder.Entity<Project>()
    // .Property(p => p.Name)
    // .HasParameterName("ProjectName");

    //    The final DbContext class is as follows


    // public class EFContext : DbContext
    //    {
    //        public EFContext() : base("EFDatabase")
    //        {
    //            Database.SetInitializer<EFContext>(new DropCreateDatabaseAlways<EFContext>());
    //        }
    //        protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //        {
    //            //Column Name
    //            modelBuilder.Entity<Employee>()
    //                      .Property(p => p.Name)
    //                      .HasColumnName("EmployeeName");

    //            //Column Order. Name will be the first column in Table
    //            modelBuilder.Entity<Employee>()
    //                      .Property(p => p.Name)
    //                      .HasColumnOrder(1);

    //            //Data Type 
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Name)
    //                .HasColumnType("varchar");

    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.DOB)
    //                .HasColumnType("Date");

    //            //Optional
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Address)
    //                .IsOptional();

    //            //Required
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Email)
    //                .IsOptional();

    //            //MaxLength
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Address)
    //                .HasMaxLength(100);

    //            //IsMaxLength
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Remark)
    //                .IsMaxLength();

    //            //IsFixedLength
    //            modelBuilder.Entity<Department>()
    //                .Property(p => p.Manager)
    //                .HasMaxLength(50)
    //                .IsFixedLength();

    //            //Ignore
    //            modelBuilder.Entity<Project>()
    //                .Ignore(e => e.NoOfEmployee);

    //            //HasPrecision
    //            modelBuilder.Entity<Project>()
    //                .Property(p => p.Budget)
    //                .HasPrecision(10, 2);

    //            //IsConcurrencyToken
    //            modelBuilder.Entity<Employee>()
    //                .Property(t => t.Name)
    //                .IsConcurrencyToken();

    //            //IsRowVersion
    //            modelBuilder.Entity<Employee>()
    //                .Property(t => t.RowID)
    //                .IsRowVersion();

    //            //UniCode
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Remark)
    //                .IsUnicode(true);

    //            //DatabaseGeneratedOption
    //            modelBuilder.Entity<Project>()
    //                .Property(p => p.ProjectID)
    //                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

    //            //DatabaseGeneratedOption.Computed
    //            //DatabaseGeneratedOption.Identity

    //            //HasParameterName
    //            modelBuilder.Entity<Project>()
    //                .Property(p => p.Name)
    //                .HasParameterName("ProjectName");

    //            base.OnModelCreating(modelBuilder);
    //        }

    //        public DbSet<Employee> Employees { get; set; }
    //        public DbSet<Department> Departments { get; set; }
    //        public DbSet<Project> Project { get; set; }


    //    }


    // public class EFContext : DbContext
    //    {
    //        public EFContext() : base("EFDatabase")
    //        {
    //            Database.SetInitializer<EFContext>(new DropCreateDatabaseAlways<EFContext>());
    //        }
    //        protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //        {
    //            //Column Name
    //            modelBuilder.Entity<Employee>()
    //                      .Property(p => p.Name)
    //                      .HasColumnName("EmployeeName");

    //            //Column Order. Name will be the first column in Table
    //            modelBuilder.Entity<Employee>()
    //                      .Property(p => p.Name)
    //                      .HasColumnOrder(1);

    //            //Data Type 
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Name)
    //                .HasColumnType("varchar");

    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.DOB)
    //                .HasColumnType("Date");

    //            //Optional
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Address)
    //                .IsOptional();

    //            //Required
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Email)
    //                .IsOptional();

    //            //MaxLength
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Address)
    //                .HasMaxLength(100);

    //            //IsMaxLength
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Remark)
    //                .IsMaxLength();

    //            //IsFixedLength
    //            modelBuilder.Entity<Department>()
    //                .Property(p => p.Manager)
    //                .HasMaxLength(50)
    //                .IsFixedLength();

    //            //Ignore
    //            modelBuilder.Entity<Project>()
    //                .Ignore(e => e.NoOfEmployee);

    //            //HasPrecision
    //            modelBuilder.Entity<Project>()
    //                .Property(p => p.Budget)
    //                .HasPrecision(10, 2);

    //            //IsConcurrencyToken
    //            modelBuilder.Entity<Employee>()
    //                .Property(t => t.Name)
    //                .IsConcurrencyToken();

    //            //IsRowVersion
    //            modelBuilder.Entity<Employee>()
    //                .Property(t => t.RowID)
    //                .IsRowVersion();

    //            //UniCode
    //            modelBuilder.Entity<Employee>()
    //                .Property(p => p.Remark)
    //                .IsUnicode(true);

    //            //DatabaseGeneratedOption
    //            modelBuilder.Entity<Project>()
    //                .Property(p => p.ProjectID)
    //                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

    //            //DatabaseGeneratedOption.Computed
    //            //DatabaseGeneratedOption.Identity

    //            //HasParameterName
    //            modelBuilder.Entity<Project>()
    //                .Property(p => p.Name)
    //                .HasParameterName("ProjectName");

    //            base.OnModelCreating(modelBuilder);
    //        }

    //        public DbSet<Employee> Employees { get; set; }
    //        public DbSet<Department> Departments { get; set; }
    //        public DbSet<Project> Project { get; set; }


    //    }





}
