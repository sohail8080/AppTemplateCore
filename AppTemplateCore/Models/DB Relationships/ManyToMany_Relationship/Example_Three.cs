using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.ManyToMany_Relationshipnbnbnv
{

    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public Object Author { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }

    public class BookCategory
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }




    // By Model Builder extension method
    public static class ModelBuilderExtensions
    {
        public static void ConfigureBookCategoryEntity(this ModelBuilder modelBuilder)
        {
            // compostie key
            modelBuilder.Entity<BookCategory>()
                        .HasKey(bc => new { bc.BookId, bc.CategoryId });

            // configure one end
            modelBuilder.Entity<BookCategory>()
                        .HasOne(bc => bc.Book)
                        .WithMany(b => b.BookCategories)
                        .HasForeignKey(bc => bc.BookId);

            // configure second end
            modelBuilder.Entity<BookCategory>()
                        .HasOne(bc => bc.Category)
                        .WithMany(c => c.BookCategories)
                        .HasForeignKey(bc => bc.CategoryId);
        }
    }



    public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            // composite key
            builder
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            // configure one end
            builder
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);

            // configure second end
            builder
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

        }
    }



}
