using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToOne_Relationship222111
{

    //When configuring the relationship with the Fluent API, you use the HasOne and WithOne methods.

    //When configuring the foreign key you need to specify the dependent entity type - 
    //notice the generic parameter provided to HasForeignKey in the listing below.
    //In a one-to-many relationship it is clear that the entity with the reference navigation 
    //is the dependent and the one with the collection is the principal. 
    //But this is not so in a one-to-one relationship - hence the need to explicitly define it.


    class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.BlogImage)// one blog has one Image
                .WithOne(i => i.Blog) // One image is related ot one blog
                .HasForeignKey<BlogImage>(b => b.BlogForeignKey);// fkey (BlogForeignKey) in BlogImage table, 
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }

        public string Url { get; set; }

        public BlogImage BlogImage { get; set; }
    }

    public class BlogImage
    {
        public int BlogImageId { get; set; }

        public byte[] Image { get; set; }

        public string Caption { get; set; }

        public int BlogForeignKey { get; set; }

        public Blog Blog { get; set; }
    }
}
