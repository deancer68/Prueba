using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Advertisement> Advertisement { get; set; }

        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Property(x => x.FirstName).HasMaxLength(60).IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.LastName).HasMaxLength(60).IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.Email).HasMaxLength(60).IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.Phone).HasMaxLength(20).IsRequired();
            
            modelBuilder.Entity<Advertisement>().Property(x=>x.Description).HasMaxLength(600).IsRequired();
            modelBuilder.Entity<Advertisement>().Property(x => x.Price).HasPrecision(12,2).IsRequired();

            modelBuilder.Entity<Category>().Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
