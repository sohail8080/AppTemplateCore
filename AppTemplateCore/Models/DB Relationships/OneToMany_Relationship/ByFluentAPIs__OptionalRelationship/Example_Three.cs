using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByFluentAPIs__OptionalRelationship
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company Company { get; set; }
    }


    //this configuration will result in an optional relationship.
    //A foreign key shadow property named CompanyId will be introduced by EF Core 
    // to the Employee entity which will be nullable.
    //In addition, the referential action constraint on the relationship will be 
    //set to NoAction.

    //Cascading Referential Integrity Constraints
    //If the relationship is configured as optional, 
    //the default behavour of EF Core is to take no action in respect of the 
    //dependent entity if the principal is deleted.
    //Consequently, if you delete the principal, 
    //any dependents will be left with foreign key values that reference principals 
    //that no longer exist.The default behaviour of a database is to raise 
    //an error in this scenario: foreign key values must reference existing primary key 
    //values.


    //You can alter this behaviour through the OnDelete method which takes a 
    //DeleteBehaviour enumeration. The following example sets the foreign key value 
    //of the dependent entity to null in the event that the principal is deleted:

    // OnDelete() define the delete behaviour of relationship
    // It has nothing to do with the relationship
    // By default this value is DeleteBehavior.Cascade

    // By Model Builder extension method
    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {
            // One 
            modelBuilder.Entity<Company>()
                        .HasMany(c => c.Employees)
                        .WithOne(e => e.Company)
                         .IsRequired(false)
                        .OnDelete(DeleteBehavior.SetNull);

            // Or this One
            modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Company)
                        .WithMany(c => c.Employees)
                         .IsRequired(false)
                        .OnDelete(DeleteBehavior.SetNull);


        }
    }



    // By Entity Configuration Class
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                 .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }


    // By Entity Configuration Class
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                 .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }



}
