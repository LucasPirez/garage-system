using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Test
{
    [Collection("IntegrationTest")]
    public class EFRepairOrderRepositoryTests : IAsyncLifetime
    {
        private readonly DbSet<EFRepairOrder> _dbSetRepairOrder;
        private readonly AppDbContext _context;
        private readonly IRepairOrderRepository _repository;
        private static readonly Guid customerId = Guid.NewGuid();
        private static readonly Guid vehicleId = Guid.NewGuid();
        private static readonly Guid workshopId = Guid.Parse(SeedData.workshopAId);

        private readonly EFCustomer customerInDb = new EFCustomer()
        {
            Id = customerId,
            FirstName = "Cliente",
            LastName = "Test",
            WorkShopId = workshopId,
            CreatedAt = DateTime.Now,
            Address = "Dirección",
        };

        private readonly EFVehicle vehicleInDb = new EFVehicle()
        {
            Id = vehicleId,
            Plate = "TEST123",
            Model = "ModeloTest",
            Color = "Negro",
            CustomerId = customerId,
        };

        private readonly RepairOrder repairOrderTest;

        public EFRepairOrderRepositoryTests(TestStartup testStartup)
        {
            var services = testStartup.Services.BuildServiceProvider();

            _context = services.GetRequiredService<AppDbContext>();
            _dbSetRepairOrder = _context.Set<EFRepairOrder>();
            _repository = services.GetRequiredService<IRepairOrderRepository>();

            repairOrderTest = new RepairOrder(
                id: Guid.NewGuid(),
                recepcionDate: DateTime.UtcNow,
                deliveryDate: null,
                cause: "No enciende",
                details: "No arranca por la mañana",
                budget: 1000,
                finalAmount: 0,
                vehicle: new Vehicle(
                    id: vehicleInDb.Id,
                    plate: vehicleInDb.Plate,
                    model: vehicleInDb.Model,
                    customerId: vehicleInDb.CustomerId
                ),
                spareParts: new List<SparePart>(),
                workshopId: workshopId
            );
        }

        [Fact]
        public async Task CreateRepairOrder_Success()
        {
            // Act
            await _repository.CreateAsync(repairOrderTest);
            var fetched = await _dbSetRepairOrder.FirstOrDefaultAsync(ro =>
                ro.Id == repairOrderTest.Id
            );

            // Assert
            Assert.NotNull(fetched);
            Assert.Equal("No enciende", fetched.Cause);
        }

        [Fact]
        public async Task GetByIdRepairOrder_ShouldReturnRepairOrder()
        {
            // Arrange
            var efRepairOrder = new EFRepairOrder
            {
                Id = Guid.NewGuid(),
                ReceptionDate = DateTime.UtcNow,
                Cause = "Motor",
                Details = "Falla de motor",
                Budget = 2000,
                FinalAmount = 0,
                WorkShopId = workshopId,
                VehicleId = vehicleId,
                Status = EFRepairOrderStatus.InProgress,
            };

            _dbSetRepairOrder.Add(efRepairOrder);
            await _context.SaveChangesAsync();

            // Act
            var repairOrder = await _repository.GetByIdAsync(efRepairOrder.Id);

            // Assert
            Assert.NotNull(repairOrder);
            Assert.Equal(efRepairOrder.Id, repairOrder.Id);
            Assert.Equal(efRepairOrder.Cause, repairOrder.Cause);
            Assert.Equal(efRepairOrder.Details, repairOrder.Details);
        }

        [Fact]
        public async Task GetByIdRepairOrder_ShouldReturnNull_WhenIdNotFound()
        {
            // Act
            var repairOrder = await _repository.GetByIdAsync(Guid.NewGuid());
            var e = _context.Vehicles.FirstOrDefaultAsync(k => k.Id == vehicleId);

            Assert.NotNull(e);

            // Assert
            Assert.Null(repairOrder);
        }

        [Fact]
        public async Task GetByVehicleIdAsync_ShouldReturnListRepairOrder()
        {
            // Arrange
            var efRepairOrder1 = new EFRepairOrder
            {
                Id = Guid.NewGuid(),
                ReceptionDate = DateTime.UtcNow,
                Cause = "Motor",
                Details = "Falla de motor",
                Budget = 2000,
                FinalAmount = 0,
                WorkShopId = workshopId,
                VehicleId = vehicleId,
                Status = EFRepairOrderStatus.InProgress,
            };
            var efRepairOrder2 = new EFRepairOrder
            {
                Id = Guid.NewGuid(),
                ReceptionDate = DateTime.UtcNow,
                Cause = "Frenos",
                Details = "Falla de frenos",
                Budget = 1500,
                FinalAmount = 0,
                WorkShopId = workshopId,
                VehicleId = vehicleId,
                Status = EFRepairOrderStatus.InProgress,
            };
            EFVehicle Othervehicle = new EFVehicle()
            {
                Id = Guid.NewGuid(),
                Plate = "TEST234",
                Model = "ModeloTest",
                Color = "Negro",
                CustomerId = customerId,
            };
            var efRepairOrder3_DiferentVehicle = new EFRepairOrder
            {
                Id = Guid.NewGuid(),
                ReceptionDate = DateTime.UtcNow,
                Cause = "Frenos",
                Details = "Falla de frenos",
                Budget = 1500,
                FinalAmount = 0,
                WorkShopId = workshopId,
                VehicleId = Othervehicle.Id,
                Status = EFRepairOrderStatus.InProgress,
            };

            _context.Vehicles.Add(Othervehicle);
            _dbSetRepairOrder.Add(efRepairOrder3_DiferentVehicle);
            _dbSetRepairOrder.Add(efRepairOrder1);
            _dbSetRepairOrder.Add(efRepairOrder2);
            await _context.SaveChangesAsync();

            // Act
            var repairOrders = await _repository.GetByVehicleIdAsync(vehicleId);

            // Assert
            Assert.NotNull(repairOrders);
            Assert.Equal(2, repairOrders.Count());
        }

        [Fact]
        public async Task UpdateRepairOrder_Success()
        {
            // Arrange
            var efRepairOrder = new EFRepairOrder
            {
                Id = Guid.NewGuid(),
                ReceptionDate = DateTime.UtcNow,
                Cause = "Motor",
                Details = "Falla de motor",
                Budget = 2000,
                FinalAmount = 0,
                WorkShopId = workshopId,
                VehicleId = vehicleId,
                Status = EFRepairOrderStatus.InProgress,
            };

            _dbSetRepairOrder.Add(efRepairOrder);
            await _context.SaveChangesAsync();

            var repairOrderToUpdate = new RepairOrder(
                id: efRepairOrder.Id,
                recepcionDate: efRepairOrder.ReceptionDate,
                deliveryDate: null,
                cause: "Motor actualizado",
                details: "Detalles actualizados",
                budget: 2500,
                finalAmount: 100,
                vehicle: new Vehicle(
                    id: vehicleInDb.Id,
                    plate: vehicleInDb.Plate,
                    model: vehicleInDb.Model,
                    customerId: vehicleInDb.CustomerId
                ),
                spareParts: new List<SparePart>(),
                workshopId: workshopId
            );

            // Act
            await _repository.UpdateAsync(repairOrderToUpdate);

            // Assert
            var updated = await _dbSetRepairOrder.FirstOrDefaultAsync(ro =>
                ro.Id == efRepairOrder.Id
            );
            Assert.NotNull(updated);
            Assert.Equal("Motor actualizado", updated.Cause);
            Assert.Equal("Detalles actualizados", updated.Details);
            Assert.Equal(2500, updated.Budget);
            Assert.Equal(100, updated.FinalAmount);
        }

        public async Task InitializeAsync()
        {
            _context.Customers.Add(customerInDb);
            _context.Vehicles.Add(vehicleInDb);
            await _context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await _context.ResetDatabase();
        }
    }
}
