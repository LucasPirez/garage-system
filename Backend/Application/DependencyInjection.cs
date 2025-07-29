using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Application.AutoMapper.AutoMapping));
            services.AddScoped<CustomerService>();
        }
    }
}
