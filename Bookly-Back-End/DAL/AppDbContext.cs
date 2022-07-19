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
    }
}
