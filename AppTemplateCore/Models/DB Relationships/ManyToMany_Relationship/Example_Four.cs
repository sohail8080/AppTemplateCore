using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.Configuring_DBSchema.ManyToMany_Relationshipmmmjjj
{
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



    // By Model Builder extension method
    public static class ModelBuilderExtensions
    {
        public static void ConfigurePostTagEntity(this ModelBuilder modelBuilder)
        {
            // compsite key for surrogate table
            modelBuilder.Entity<PostTag>().HasKey(p => new { p.PostId, p.TagId });

            // Configure one end of relationship
            modelBuilder.Entity<PostTag>().HasOne(ss => ss.Post)
                .WithMany(s => s.PostTags)
                .HasForeignKey(ss => ss.PostId);

            // Configure second end of relationship
            modelBuilder.Entity<PostTag>().HasOne(ss => ss.Tag)
                .WithMany(s => s.PostTags)
                .HasForeignKey(ss => ss.TagId);
        }
    }




    public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            // composite key sfor surrogate table
            builder.HasKey(s => new { s.PostId, s.TagId });

            // Configure one end of relationship
            builder.HasOne(ss => ss.Post)
                .WithMany(s => s.PostTags)
                .HasForeignKey(ss => ss.PostId);

            // Configure second end of relationship
            builder.HasOne(ss => ss.Tag)
                .WithMany(s => s.PostTags)
                .HasForeignKey(ss => ss.TagId);
        }
    }


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

}
