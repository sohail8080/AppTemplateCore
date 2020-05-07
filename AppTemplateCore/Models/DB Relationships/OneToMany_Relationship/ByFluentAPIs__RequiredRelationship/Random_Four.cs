using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByFluentAPIs__RequiredRelationship2000
{
    //You can use the Fluent API to configure which properties should be used as the composite foreign key properties 
    //for a given relationship:

    class MyContext9999 : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasKey(c => new { c.State, c.LicensePlate });

            modelBuilder.Entity<RecordOfSale>()
                .HasOne(s => s.Car)
                .WithMany(c => c.SaleHistory)
                .HasForeignKey(s => new { s.CarState, s.CarLicensePlate });
        }
    }

    public class Car
    {
        public string State { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public List<RecordOfSale> SaleHistory { get; set; }
    }

    public class RecordOfSale
    {
        public int RecordOfSaleId { get; set; }
        public DateTime DateSold { get; set; }
        public decimal Price { get; set; }

        public string CarState { get; set; }
        public string CarLicensePlate { get; set; }
        public Car Car { get; set; }
    }
}
