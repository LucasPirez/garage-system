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
            IConfiguration? configuration = default
        )
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (configuration is not null)
            {
                var connection = configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));
            }
            else
            {
                Console.WriteLine(
                    $"[Warning] Configuration is null in {nameof(AddInfraestructure)}"
                );
            }

            services.AddScoped<ICustomerRepository, EFCustomerRepository>();
            services.AddScoped<IVehicleRepository, EFVehicleRepository>();
        }
    }
}
