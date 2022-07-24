using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Slayd> Slayds { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookFormat> BookFormats { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<BookLanguage> BookLanguages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<AuthorAward> AuthorAwards { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<AuthorSocialMedia> AuthorSocialMedias { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Discount> Discounts { get; set; }
    }
}
