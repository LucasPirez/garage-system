using System.Reflection;
using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddInfraestructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var connection = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine("ASPNETROCE_ENVIRONMENT -------------- " + env);
            services.AddDbContext<AppDbContext>(options =>
            {
                if (env == "Testing")
                    options.UseSqlite(connection);
                else
                    options.UseNpgsql(connection);
            });

            services.AddScoped<ICustomerRepository, EFCustomerRepository>();
        }
    }
}
