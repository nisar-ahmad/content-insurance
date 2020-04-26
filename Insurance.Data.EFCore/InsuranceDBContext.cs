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
            SeedData();
        }

        private void SeedData()
        {
            if (!Categories.Any())
            {
                var categories = new List<Category>()
                {
                   new Category { Name = "Clothing" },
                   new Category { Name = "Electronics" },
                   new Category { Name = "Kitchen" }
                };
                Categories.AddRange(categories);
                SaveChanges();
            }
        }
    }
}
