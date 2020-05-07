using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.ManyToMany_Relationshipmmmjjj
{
    class MyContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  composite key
            modelBuilder.Entity<PostTag>()
                .HasKey(t => new { t.PostId, t.TagId });

            // configure one end
            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);

            // configure second end
            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        //NP
        public List<PostTag> PostTags { get; set; }
    }

    public class Tag
    {
        public string TagId { get; set; }

        //NP
        public List<PostTag> PostTags { get; set; }
    }

    public class PostTag
    {
        //NP + Id
        public int PostId { get; set; }
        public Post Post { get; set; }

        //NP + Id
        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
