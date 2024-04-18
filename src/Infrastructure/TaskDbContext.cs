using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Infrastructure
{
    public class TaskDbContext : DbContext
    {

        public TaskDbContext() : base() { }
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Retrieve the connection string from app.config using ConfigurationManager
                string connectionString = "";
                if (ConfigurationManager.ConnectionStrings["DefaultConnection"] != null )
                     connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                // Configure DbContext to use SQL Server with the retrieved connection string
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        // Define DbSet properties for each entity
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }

        // Configure the model using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.FirstName);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.LastName);

            base.OnModelCreating(modelBuilder);

            // Configure relationships and other model settings here

            // For example, you can configure a one-to-many relationship between Customer and Contact
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Contacts)
                .WithOne(con => con.Customer)
                .HasForeignKey(con => con.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Similarly, configure a one-to-many relationship between Customer and Task
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Tasks)
                .WithOne(t => t.Customer)
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
