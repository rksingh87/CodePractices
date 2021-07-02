using Microsoft.EntityFrameworkCore;
using PostgreSqlConcurrencyCheck.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostgreSqlConcurrencyCheck.Database
{
    public class PostgresSqlDbContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }


        public PostgresSqlDbContext(DbContextOptions<PostgresSqlDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<Employee>()
                .UseXminAsConcurrencyToken()
                .HasIndex(t => t.name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
