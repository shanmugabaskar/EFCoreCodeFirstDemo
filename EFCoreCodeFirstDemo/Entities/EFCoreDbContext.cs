using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        // The OnConfiguring method allows us to configure the DbContext options,
        // such as specifying the database provider and connection string.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Display the generated SQL queries in the Console window
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);


            // Step 1: Load the Configuration File (appsettings.json).
            // The ConfigurationBuilder class is used to construct configuration settings from various sources.
            // Here, we add the appsettings.json file to the configuration sources and then build it.
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // Specify the configuration file to load.
                .Build(); // Build the configuration object, making it ready to retrieve values.
            // Step 2: Get the "ConnectionStrings" section from the configuration.
            // The GetSection method is used to access a specific section within the configuration file.
            // Here, we are accessing the "ConnectionStrings" section which contains our database connection strings.
            var configSection = configBuilder.GetSection("ConnectionStrings");
            // Step 3: Retrieve the connection string value using its key ("SQLServerConnection").
            // The indexer [] is used to access the value corresponding to the "SQLServerConnection" key within the section.
            // The null-coalescing operator (??) ensures that if the key is not found, it will return null.
            var connectionString = configSection["SQLServerConnection"] ?? null;
            // Step 4: Configure the DbContext to use SQL Server with the retrieved connection string.
            // The UseSqlServer method is an extension method that configures the context to connect to a SQL Server database.
            optionsBuilder.UseSqlServer(connectionString);
        }
        // DbSet<Student> Students represents a table in the database corresponding to the Student entity.
        // EF Core uses DbSet<TEntity> to track changes and execute queries related to the Student entity.
        public DbSet<Student> Students { get; set; }
        // DbSet<Branch> Branches represents a table in the database corresponding to the Branch entity.
        // Similar to the Students DbSet, this property is used by EF Core to track and manage Branch entities.
        public DbSet<Branch> Branches { get; set; }
    }
}
