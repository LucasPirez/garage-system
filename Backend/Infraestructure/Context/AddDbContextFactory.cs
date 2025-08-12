using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile("appsettings.Development.json", optional: true)
                    .Build();

                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string is not set in environment variables."
                );
            }
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
