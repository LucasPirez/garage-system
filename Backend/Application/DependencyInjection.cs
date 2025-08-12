using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Application.AutoMapper.AutoMapping));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IRepairOrderService, RepairOrderService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}
