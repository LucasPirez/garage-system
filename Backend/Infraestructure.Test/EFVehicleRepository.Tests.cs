using System.Data.Common;
using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Infraestructure.Test
{
    [Collection("IntegrationTest")]
    public class EFVehicleRepositoryTests : IDisposable
    {
        private readonly DbSet<EFVehicle> _dbSetVehicle;
        private readonly AppDbContext _context;
        private readonly IVehicleRepository _repository;
        private static readonly Guid customerId = Guid.NewGuid();
        private readonly EFCustomer customerInDb = new EFCustomer()
        {
            Id = customerId,
            FirstName = "customre",
            LastName = "Customer",
            WorkShopId = Guid.Parse(SeedData.workshopAId),
            CreatedAt = DateTime.Now,
            Address = "Dirreccion",
        };
        private readonly Vehicle vehicleTest = new Vehicle(
            id: Guid.NewGuid(),
            plate: "ABC123",
            model: "Sedan",
            color: "Red",
            customerId: customerId
        );

        public EFVehicleRepositoryTests(TestStartup testStartup)
        {
            var services = testStartup.Services.BuildServiceProvider();

            _context = services.GetRequiredService<AppDbContext>();
            _dbSetVehicle = _context.Set<EFVehicle>();
            _repository = services.GetRequiredService<IVehicleRepository>();

            _context.Customers.Add(customerInDb);
            _context.SaveChangesAsync().Wait();
        }

        [Fact]
        public async Task CreateVehicle_Success()
        {
            // Act
            await _repository.CreateAsync(vehicleTest);
            var fetched = await _dbSetVehicle.FirstOrDefaultAsync(veh => veh.Id == vehicleTest.Id);

            // Assert
            Assert.NotNull(fetched);
            Assert.Equal("ABC123", fetched.Plate);
        }

        [Fact]
        public async Task GetByIdVehicle_ShouldReturnVehicle()
        {
            // Arrange
            var efVehicle = new EFVehicle
            {
                Id = Guid.NewGuid(),
                Plate = "XYZ789",
                Model = "SUV",
                Color = "Blue",
                CustomerId = customerInDb.Id,
            };

            _dbSetVehicle.Add(efVehicle);
            await _context.SaveChangesAsync();

            // Act
            var vehicle = await _repository.GetByIdAsync(efVehicle.Id);

            // Assert
            Assert.NotNull(vehicle);
            Assert.Equal(efVehicle.Id, vehicle.Id);
            Assert.Equal(efVehicle.Plate, vehicle.Plate);
            Assert.Equal(efVehicle.Model, vehicle.Model);
        }

        [Fact]
        public async Task CreateVehicle_ShouldThrowException_WhenIdAlreadyExists()
        {
            // Arrange
            var efVehicle = new EFVehicle
            {
                Id = vehicleTest.Id,
                Plate = "XYZ789",
                Model = "SUV",
                Color = "Blue",
                CustomerId = vehicleTest.CustomerId,
            };

            _dbSetVehicle.Add(efVehicle);
            await _context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _repository.CreateAsync(vehicleTest)
            );
        }

        [Fact]
        public async Task GetByIdVehicle_ShouldReturnNull_WhenIdNotFound()
        {
            // Act
            var vehicle = await _repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(vehicle);
        }

        [Fact]
        public async Task UpdateVehicle_Success()
        {
            // Arrange
            var efVehicle = new EFVehicle
            {
                Id = Guid.NewGuid(),
                Plate = "XYZ789",
                Model = "SUV",
                Color = "Blue",
                CustomerId = customerInDb.Id,
            };

            _dbSetVehicle.Add(efVehicle);
            await _context.SaveChangesAsync();

            var vehicleToUpdate = new Vehicle(
                id: efVehicle.Id,
                plate: "XYZ789-Updated",
                model: "SUV-Updated",
                color: "Green",
                customerId: efVehicle.CustomerId
            );

            // Act
            await _repository.UpdateAsync(vehicleToUpdate);

            // Assert
            var updatedVehicle = await _dbSetVehicle.FirstOrDefaultAsync(veh =>
                veh.Id == efVehicle.Id
            );
            Assert.NotNull(updatedVehicle);
            Assert.Equal(vehicleToUpdate.Plate, updatedVehicle.Plate);
            Assert.Equal(vehicleToUpdate.Model, updatedVehicle.Model);
            Assert.Equal(vehicleToUpdate.Color, updatedVehicle.Color);
        }

        public void Dispose()
        {
            _context.ResetDatabase();
        }
    }
}
