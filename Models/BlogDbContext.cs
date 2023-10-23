using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogWebAPI.Models
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Article> Articles { set; get; }
        public DbSet<Tag> Tags { get; set; }
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>()
                    .HasMany(t => t.Articles)
                    .WithMany(a => a.Tags)
                    .UsingEntity<ArticlesTags>(
                        j => j
                            .HasOne(at => at.Article)
                            .WithMany(a => a.ArticleTag)
                            .HasForeignKey(at => at.ArticleId),
                        j => j
                            .HasOne(at => at.Tag)
                            .WithMany(t => t.ArticleTag)
                            .HasForeignKey(at => at.TagId),
                        j =>
                        {
                            j.HasKey(k => new { k.ArticleId, k.TagId });
                    });
        }
    }
}
