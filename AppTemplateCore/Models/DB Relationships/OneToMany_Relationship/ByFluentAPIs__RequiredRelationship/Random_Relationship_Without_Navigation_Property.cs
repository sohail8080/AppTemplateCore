using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.OneToMany_Relationship.ByFluentAPIs__RequiredRelationshipsdfgh
{
    //Without navigation property
    //You don't necessarily need to provide a navigation property. You can simply provide a foreign key on one side of the relationship.

    class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne<Blog>()
                .WithMany()
                .HasForeignKey(p => p.BlogId);
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
    }
}
