using System.Reflection;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using Infraestructure.Config;
using Infraestructure.Context;
using Infraestructure.Repository;
using Infraestructure.Services;
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
                services.Configure<SmtpSettings>(configuration.GetSection(SmtpSettings.Section));
            }
            else
            {
                Console.WriteLine(
                    $"[Warning] Configuration is null in {nameof(AddInfraestructure)}"
                );
            }

            services.AddScoped<ICustomerRepository, EFCustomerRepository>();
            services.AddScoped<IVehicleRepository, EFVehicleRepository>();
            services.AddScoped<IRepairOrderRepository, EFRepairOrderRepository>();
            services.AddScoped<IAdminRepository, EFAdminRepository>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

            services.AddScoped<ISmtpService, SmtpService>();
        }
    }
}
