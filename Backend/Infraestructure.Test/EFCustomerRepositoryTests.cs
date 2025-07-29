using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Test
{
    public class EFCustomerRepositoryTests : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly AppDbContext _dbContext;
        private readonly ICustomerRepository _repository;

        public EFCustomerRepositoryTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"));
            services.AddAutoMapper(typeof(Infraestructure.AutoMapper.AutoMapping));
            services.AddScoped<ICustomerRepository, EFCustomerRepository>();

            _serviceProvider = services.BuildServiceProvider();
            _dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
            _repository = _serviceProvider.GetRequiredService<ICustomerRepository>();
        }

        [Fact]
        public async Task CreateCustomer_Works()
        {
            //var workshop = new EFWorkShop() { Id = Guid.NewGuid(), Name = "Test Workshop" };
            //// Ensure the workshop is added to the context
            //_dbContext.WorkShops.Add(workshop);
            //await _dbContext.SaveChangesAsync();

            //var Admin = new EFAdmin()
            //{
            //    Email = "email@gmail.com",
            //    Password = "123456",
            //    Id = Guid.NewGuid(),
            //    WorkShopId = workshop.Id,
            //};

            //_dbContext.Admins.Add(Admin);
            //await _dbContext.SaveChangesAsync();

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = new System.Collections.Generic.List<string> { "123" },
                Email = new System.Collections.Generic.List<string> { "test@example.com" },
                Address = "Test Address",
                Dni = "12345678",
                WorkShopId = Guid.NewGuid(),
            };

            await _repository.CreateAsync(customer);
            var fetched = await _dbContext.Customers.FirstOrDefaultAsync(cus =>
                cus.Id == customer.Id
            );
            Assert.NotNull(fetched);
            Assert.Equal("Test", fetched.FirstName);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
            _serviceProvider.Dispose();
        }
    }
}
