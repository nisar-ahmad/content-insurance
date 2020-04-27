using Insurance.Models.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Insurance.Data.EFCore
{
    /// <summary>
    /// This is needed since DBContext is defined in a class library project
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InsuranceDBContext>
    {
        /// <summary>
        /// Creates DB Context
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public InsuranceDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../Insurance.Api/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<InsuranceDBContext>();
            var connectionString = configuration.GetConnectionString("InsuranceDBContext");
            builder.UseSqlServer(connectionString);
            return new InsuranceDBContext(builder.Options);
        }
    }

    /// <summary>
    /// Insurance DB Context
    /// </summary>
    public class InsuranceDBContext : DbContext
    {
        /// <summary>
        /// Categories Table
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Items Table
        /// </summary>
        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public InsuranceDBContext(DbContextOptions<InsuranceDBContext> options) : base(options)
        {
        }

        public void SeedData()
        {
            Database.EnsureCreated();

            if (Categories.Any())
                return;

            var categories = new List<Category>()
            {
                new Category { Name = "Clothing" },
                new Category { Name = "Electronics" },
                new Category { Name = "Kitchen" },
                new Category { Name = "Other" }
            };

            Categories.AddRange(categories);
            SaveChanges();

            var items = new List<Item>
            {
                // Clothing
                new Item{ Name = "Shirts", Value = 1100, CategoryId = categories[0].CategoryId },
                new Item{ Name = "Jeans", Value = 1100, CategoryId = categories[0].CategoryId },

                // Electronics
                new Item{ Name = "TV", Value = 2000, CategoryId = categories[1].CategoryId },
                new Item{ Name = "Playstation", Value = 400, CategoryId = categories[1].CategoryId },
                new Item{ Name = "Stereo", Value = 1600, CategoryId = categories[1].CategoryId },


                new Item{ Name = "Pots and Pans", Value = 3000, CategoryId = categories[2].CategoryId },
                new Item{ Name = "Flatware", Value = 500, CategoryId = categories[2].CategoryId },
                new Item{ Name = "Knife Set", Value = 500, CategoryId = categories[2].CategoryId },
                new Item{ Name = "Misc", Value = 1000, CategoryId = categories[2].CategoryId },
            };

            Items.AddRange(items);
            SaveChanges();
        }
    }
}
