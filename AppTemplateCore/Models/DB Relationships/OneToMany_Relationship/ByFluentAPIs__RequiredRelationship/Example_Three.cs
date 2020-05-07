using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByFluentAPIs__OptionalRelationship222
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


    //this configuration will result in an required relationship.
    //A foreign key shadow property named CompanyId will be introduced by EF Core 
    // to the Employee entity which will be not nullable.
    //In addition, the referential action constraint on the relationship will be 
    //set to Cascade.

    // By Model Builder extension method
    public static class ModelBuilderExtensions
    {
        public static void ConfigureStudentEntity(this ModelBuilder modelBuilder)
        {

            // One 
            modelBuilder.Entity<Company>()
                        .HasMany(c => c.Employees)
                        .WithOne(e => e.Company)
                        .IsRequired();

            // Or this One
            modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Company)
                        .WithMany(c => c.Employees)
                        .IsRequired();

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
                .IsRequired();

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
                .IsRequired();

        }
    }



}
