using System.Data.Common;
using System.Net;
using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Test
{
    [Collection("IntegrationTest")]
    public class EFCustomerRepositoryTests : IDisposable
    {
        private readonly DbSet<EFCustomer> _dbSetCustomer;
        private readonly AppDbContext _context;
        private readonly ICustomerRepository _repository;
        private TestStartup _testStartup;
        private Customer customerTest = new Customer(
            id: Guid.NewGuid(),
            firstName: "Test",
            lastName: "User",
            phoneNumbers: new List<string> { "123" },
            emails: new List<string> { "test@example.com" },
            address: "Test Address",
            dni: "12345678",
            workshopId: Guid.Parse(SeedData.workshopAId)
        );

        public EFCustomerRepositoryTests(TestStartup testStartup)
        {
            _testStartup = testStartup;
            var services = _testStartup.Services.BuildServiceProvider();

            _context = services.GetRequiredService<AppDbContext>();
            _dbSetCustomer = _context.Set<EFCustomer>();
            _repository = services.GetRequiredService<ICustomerRepository>();
        }

        [Fact]
        public async Task CreateCustomer_Success()
        {
            await _repository.CreateAsync(customerTest);
            var fetched = await _dbSetCustomer.FirstOrDefaultAsync(cus =>
                cus.Id == customerTest.Id
            );
            Assert.NotNull(fetched);
            Assert.Equal("Test", fetched.FirstName);
        }

        [Fact]
        public async Task CreateCustomer_ShouldThrowException_WhenIdAlreadyExists()
        {
            var EFCustomer = new EFCustomer
            {
                Id = customerTest.Id,
                FirstName = "other-customer",
                LastName = "other-customer",
                PhoneNumber = new List<string> { "123" },
                Email = new List<string> { "test@example.com" },
                Address = "Test Address",
                Dni = "12345678",
                WorkShopId = Guid.Parse(SeedData.workshopAId),
            };

            _dbSetCustomer.Add(EFCustomer);
            await _context.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _repository.CreateAsync(customerTest)
            );
        }

        [Fact]
        public async Task GetByIdCustomer_ShouldReturnCustomer()
        {
            var EFCustomer = new EFCustomer
            {
                Id = Guid.NewGuid(),
                FirstName = "other-customer",
                LastName = "other-customer",
                PhoneNumber = new List<string> { "123" },
                Email = new List<string> { "test@example.com" },
                Address = "Test Address",
                Dni = "12345678",
                WorkShopId = Guid.Parse(SeedData.workshopAId),
            };

            _dbSetCustomer.Add(EFCustomer);
            await _context.SaveChangesAsync();

            Customer? customer = await _repository.GetByIdAsync(EFCustomer.Id);

            Assert.NotNull(customer);
            Assert.Equal(EFCustomer.Id, customer.Id);
            Assert.Equal(EFCustomer.FirstName, customer.FirstName);
            Assert.Equal(EFCustomer.LastName, customer.LastName);
        }

        [Fact]
        public async Task GetByIdCustomer_ShouldReturnNull_WhenIdNotFound()
        {
            var EFCustomer = new EFCustomer
            {
                Id = Guid.NewGuid(),
                FirstName = "other-customer",
                LastName = "other-customer",
                PhoneNumber = new List<string> { "123" },
                Email = new List<string> { "test@example.com" },
                Address = "Test Address",
                Dni = "12345678",
                WorkShopId = Guid.Parse(SeedData.workshopAId),
            };

            Customer? customer = await _repository.GetByIdAsync(EFCustomer.Id);

            Assert.Null(customer);
        }

        [Fact]
        public async Task UpdateCustomer_Success()
        {
            var EFCustomer = new EFCustomer
            {
                Id = Guid.NewGuid(),
                FirstName = "other-customer",
                LastName = "other-customer",
                PhoneNumber = new List<string> { "123" },
                Email = new List<string> { "test@example.com" },
                Address = "Test Address",
                Dni = "12345678",
                WorkShopId = Guid.Parse(SeedData.workshopAId),
            };

            _dbSetCustomer.Add(EFCustomer);
            await _context.SaveChangesAsync();

            Customer customerToUpdated = new Customer(
                id: EFCustomer.Id,
                firstName: "other-customer-edited",
                lastName: "other-customer-edited",
                phoneNumbers: new List<string> { "1233423" },
                emails: new List<string> { "test-edited@example.com" },
                address: "Test Address edited",
                dni: "12345222",
                workshopId: Guid.Parse(SeedData.workshopAId)
            );

            await _repository.UpdateAsync(customerToUpdated);

            var customer = _dbSetCustomer.Where(cus => cus.Id == EFCustomer.Id).FirstOrDefault();

            Assert.NotNull(customer);
            Assert.Equal(customerToUpdated.FirstName, customer.FirstName);
            Assert.Equal(customerToUpdated.LastName, customer.LastName);
            Assert.Equal("1233423", customer.PhoneNumber.FirstOrDefault());
            Assert.Equal(customerToUpdated.Email.FirstOrDefault(), customer.Email.FirstOrDefault());
            Assert.Equal(customerToUpdated.Address, customer.Address);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldSuccess()
        {
            var EFCustomer = new EFCustomer
            {
                Id = Guid.NewGuid(),
                FirstName = "other-customer",
                LastName = "other-customer",
                PhoneNumber = new List<string> { "123" },
                Email = new List<string> { "test@example.com" },
                Address = "Test Address",
                Dni = "12345678",
                WorkShopId = Guid.Parse(SeedData.workshopAId),
            };

            _dbSetCustomer.Add(EFCustomer);
            await _context.SaveChangesAsync();

            Customer customerToDelete = new Customer(
                id: EFCustomer.Id,
                firstName: "other-customer",
                lastName: "other-customer",
                phoneNumbers: new List<string> { "1233423" },
                emails: new List<string> { "test@example.com" },
                address: "Test Address",
                dni: "12345678",
                workshopId: Guid.Parse(SeedData.workshopAId)
            );

            await _repository.DeleteAsync(customerToDelete);

            EFCustomer? customerGet = await _dbSetCustomer
                .Where(cus => cus.Id == customerToDelete.Id)
                .FirstOrDefaultAsync();

            Assert.Null(customerGet);
        }

        public void Dispose()
        {
            _testStartup.ResetDataBase();
        }
    }
}
