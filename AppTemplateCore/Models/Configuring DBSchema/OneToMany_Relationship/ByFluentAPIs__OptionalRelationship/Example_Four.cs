using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByFluentAPIs__OptionalRelationship4444
{
    public class SampleContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Method One
            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Company)
            .WithMany(c => c.Employees)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

            // Method Two
            modelBuilder.Entity<Company>()
            .HasMany(c => c.Employees)
            .WithOne(e => e.Company)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);


        }
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
    }
}