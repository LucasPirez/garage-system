using Application.Dtos.Vehicle;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Moq;

namespace Application.Test
{
    public class VehicleServiceTest
    {
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly VehicleService _vehicleService;
        private readonly Mock<IMapper> _mapperMock;

        public VehicleServiceTest()
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _mapperMock = new Mock<IMapper>();
            _vehicleService = new VehicleService(_vehicleRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            // Arrange
            var createDto = new CreateVehicleDto
            {
                Id = Guid.NewGuid(),
                Plate = "ABC123",
                Model = "Sedan",
                Color = "Red",
                CustomerId = Guid.NewGuid().ToString(),
            };

            var vehicle = new Vehicle(
                Guid.NewGuid(),
                createDto.Plate,
                Guid.Parse(createDto.CustomerId),
                createDto.Model,
                createDto.Color
            );
            _mapperMock.Setup(m => m.Map<Vehicle>(createDto)).Returns(vehicle);
            _vehicleRepositoryMock.Setup(r => r.CreateAsync(vehicle)).Returns(Task.CompletedTask);

            // Act
            await _vehicleService.CreateAsync(createDto);

            // Assert
            _mapperMock.Verify(m => m.Map<Vehicle>(createDto), Times.Once);
            _vehicleRepositoryMock.Verify(r => r.CreateAsync(vehicle), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnVehicles()
        {
            // Arrange
            var workshopId = Guid.NewGuid();
            var vehicles = new List<Vehicle>
            {
                new Vehicle(Guid.NewGuid(), "ABC123", Guid.NewGuid(), "Sedan", "Red"),
                new Vehicle(Guid.NewGuid(), "XYZ789", Guid.NewGuid(), "SUV", "Blue"),
            };

            _vehicleRepositoryMock.Setup(r => r.GetAllAsync(workshopId)).ReturnsAsync(vehicles);

            // Act
            var result = await _vehicleService.GetAllAsync(workshopId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(vehicles.Count, result.Count());
            _vehicleRepositoryMock.Verify(r => r.GetAllAsync(workshopId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnVehicle_WhenExists()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var vehicle = new Vehicle(vehicleId, "ABC123", Guid.NewGuid(), "Sedan", "Red");

            _vehicleRepositoryMock.Setup(r => r.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);

            // Act
            var result = await _vehicleService.GetByIdAsync(vehicleId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(vehicleId, result.Id);
            _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(vehicleId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenNotExists()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();

            _vehicleRepositoryMock
                .Setup(r => r.GetByIdAsync(vehicleId))
                .ReturnsAsync((Vehicle)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => _vehicleService.GetByIdAsync(vehicleId)
            );
        }

        [Fact]
        public async Task GetByPlateAsync_houldReturnVehicle_WhenExistsInWorkShop()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var vehicle = new Vehicle(vehicleId, "ABC123", Guid.NewGuid(), "Sedan", "Red");
            var workshopId = Guid.NewGuid();

            _vehicleRepositoryMock
                .Setup(r => r.GetByPlateAsync(vehicle.Plate, workshopId))
                .ReturnsAsync(vehicle);

            // Act
            var result = await _vehicleService.GetByPlateAsync(vehicle.Plate, workshopId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(vehicleId, result.Id);
            _vehicleRepositoryMock.Verify(
                r => r.GetByPlateAsync(vehicle.Plate, workshopId),
                Times.Once
            );
        }

        [Fact]
        public async Task GetByPlateAsync_ShouldThrowException_WhenNotExistsInWorkshop()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var vehicle = new Vehicle(vehicleId, "ABC123", Guid.NewGuid(), "Sedan", "Red");
            var workshopId = Guid.NewGuid();

            _vehicleRepositoryMock
                .Setup(r => r.GetByPlateAsync(vehicle.Plate, workshopId))
                .ReturnsAsync((Vehicle)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => _vehicleService.GetByIdAsync(vehicleId)
            );
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateVehicleSuccessfully()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var updateDto = new UpdateVehicleDto
            {
                Plate = "XYZ789",
                Model = "SUV",
                Color = "Blue",
            };

            var vehicle = new Vehicle(vehicleId, "ABC123", Guid.NewGuid(), "Sedan", "Red");
            _vehicleRepositoryMock.Setup(r => r.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);

            // Act
            await _vehicleService.UpdateAsync(vehicleId, updateDto);

            // Assert
            _vehicleRepositoryMock.Verify(r => r.UpdateAsync(vehicle), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenNotExistsy()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var updateDto = new UpdateVehicleDto
            {
                Plate = "XYZ789",
                Model = "SUV",
                Color = "Blue",
            };

            _vehicleRepositoryMock
                .Setup(r => r.GetByIdAsync(vehicleId))
                .ReturnsAsync((Vehicle)null);

            // Act && Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => _vehicleService.UpdateAsync(vehicleId, updateDto)
            );
            _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(vehicleId), Times.Once);
        }
    }
}
