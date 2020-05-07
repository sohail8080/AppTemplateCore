using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByFluentAPIs__RequiredRelationshipvbnm
{
    //Principal key
    //If you want the foreign key to reference a property other than the primary key, you can use the Fluent API to configure the principal key property for the relationship.The property that you configure as the principal key will automatically be setup as an alternate key.


    class MyContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordOfSale>()
                .HasOne(s => s.Car)
                .WithMany(c => c.SaleHistory)
                .HasForeignKey(s => s.CarLicensePlate)
                .HasPrincipalKey(c => c.LicensePlate);
        }
    }

    public class Car
    {
        public int CarId { get; set; }
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

        public string CarLicensePlate { get; set; }
        public Car Car { get; set; }
    }



}
