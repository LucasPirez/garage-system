using Application.AutoMapper;
using Application.Dtos.Customer;
using Application.Dtos.RepairOrder;
using Application.Dtos.Vehicle;
using Application.Services;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;
using Moq;

namespace Application.Test
{
    public class RepairOrderServiceTest
    {
        private readonly Mock<IRepairOrderRepository> _repairOrderRepositoryMock;
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<IVehicleService> _vehicleServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly RepairOrderService _service;

        private readonly Vehicle _vehicle = new Vehicle(
            id: Guid.NewGuid(),
            plate: "ABC123",
            customerId: Guid.NewGuid(),
            model: "Test Model",
            color: "Red"
        );

        public RepairOrderServiceTest()
        {
            _repairOrderRepositoryMock = new Mock<IRepairOrderRepository>();
            _customerServiceMock = new Mock<ICustomerService>();
            _vehicleServiceMock = new Mock<IVehicleService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapping>()).CreateMapper();

            _service = new RepairOrderService(
                _repairOrderRepositoryMock.Object,
                _customerServiceMock.Object,
                _vehicleServiceMock.Object,
                _unitOfWorkMock.Object,
                _mapper
            );
        }

        [Fact]
        public async Task CreateRepairOrderAsync_WithVehicleDto_CreatesRepairOrder()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var workshopId = Guid.NewGuid();
            var vehicleDto = new CreateVehicleDto
            {
                Plate = "ABC123",
                Model = "Test",
                Color = "Red",
                CustomerId = Guid.NewGuid().ToString(),
                Id = vehicleId,
            };
            var dto = new CreateRepairOrderWithVehicleDto
            {
                Id = Guid.NewGuid(),
                VehicleDto = vehicleDto,
                WorkshopId = workshopId.ToString(),
                Cause = "Test cause",
                Details = "Test details",
                ReceptionDate = DateTime.UtcNow,
            };
            _vehicleServiceMock
                .Setup(x => x.GetByPlateAsync(vehicleDto.Plate, workshopId))
                .ThrowsAsync(new EntityNotFoundException(vehicleId));
            _vehicleServiceMock.Setup(x => x.CreateAsync(vehicleDto)).Returns(Task.CompletedTask);
            _vehicleServiceMock
                .Setup(x => x.GetByIdAsync(vehicleId))
                .ReturnsAsync(
                    new VehicleDto()
                    {
                        Id = vehicleId,
                        Plate = vehicleDto.Plate,
                        CustomerId = vehicleDto.CustomerId,
                        Model = vehicleDto.Model,
                        Color = vehicleDto.Color,
                    }
                );
            _repairOrderRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<RepairOrder>()))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

            // Act
            await _service.CreateRepairOrderAsync(dto);

            // Assert
            _vehicleServiceMock.Verify(x => x.CreateAsync(vehicleDto), Times.Once);
            _repairOrderRepositoryMock.Verify(
                x => x.CreateAsync(It.IsAny<RepairOrder>()),
                Times.Once
            );
            _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateRepairOrderAsync_WithVehicleAndCustomerDto_CreatesRepairOrder()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var vehicleId = Guid.NewGuid();
            var workshopId = Guid.NewGuid();
            var customerDto = new BaseCustomerDto
            {
                Id = customerId,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = new List<string> { "123" },
                Email = new List<string> { "john@doe.com" },
                Address = "Test Address",
                Dni = "12345678",
            };
            var vehicleDto = new VehicleDto
            {
                Id = vehicleId,
                Plate = "XYZ789",
                Model = "SUV",
                Color = "Blue",
                CustomerId = customerId.ToString(),
            };
            var dto = new CreateRepairOrderWithVehicleAndCustomerDto
            {
                Id = Guid.NewGuid(),
                CustomerDto = customerDto,
                VehicleDto = vehicleDto,
                WorkshopId = workshopId.ToString(),
                Cause = "Test cause",
                Details = "Test details",
                ReceptionDate = DateTime.UtcNow,
            };
            var createCustomerDto = new CreateCustomerDto
            {
                Id = customerId,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                PhoneNumber = customerDto.PhoneNumber,
                Email = customerDto.Email,
                Address = customerDto.Address,
                Dni = customerDto.Dni,
                WorkshopId = workshopId.ToString(),
            };
            var createVehicleDto = new CreateVehicleDto
            {
                Id = vehicleId,
                Plate = vehicleDto.Plate,
                Model = vehicleDto.Model,
                Color = vehicleDto.Color,
                CustomerId = customerId.ToString(),
            };

            _vehicleServiceMock
                .Setup(x => x.GetByPlateAsync(vehicleDto.Plate, workshopId))
                .ThrowsAsync(new EntityNotFoundException(vehicleId));

            _vehicleServiceMock
                .Setup(x => x.GetByIdAsync(vehicleId))
                .ReturnsAsync(
                    new VehicleDto()
                    {
                        Id = vehicleId,
                        Plate = vehicleDto.Plate,
                        CustomerId = vehicleDto.CustomerId,
                        Model = vehicleDto.Model,
                        Color = vehicleDto.Color,
                    }
                );
            _repairOrderRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<RepairOrder>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(x => x.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);

            // Act
            await _service.CreateRepairOrderAsync(dto);

            // Assert
            _customerServiceMock.Verify(
                x => x.CreateAsync(It.IsAny<CreateCustomerDto>()),
                Times.Once
            );
            _vehicleServiceMock.Verify(
                x => x.CreateAsync(It.IsAny<CreateVehicleDto>()),
                Times.Once
            );
            _repairOrderRepositoryMock.Verify(
                x => x.CreateAsync(It.IsAny<RepairOrder>()),
                Times.Once
            );
            _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateRepairOrderAsync_Successfuly()
        {
            // Arrange
            var createDto = new CreateRepairOrderDto
            {
                Id = Guid.NewGuid(),
                ReceptionDate = DateTime.UtcNow,
                Cause = "Test cause",
                Details = "Test details",
                Vehicle = new Mock<VehicleDto>().Object,
                WorkshopId = Guid.NewGuid().ToString(),
            };

            // Act
            await _service.CreateRepairOrderAsync(createDto);

            // Assert
            _repairOrderRepositoryMock.Verify(
                x => x.CreateAsync(It.IsAny<RepairOrder>()),
                Times.Once
            );
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRepairOrder()
        {
            // Arrange
            var id = Guid.NewGuid();

            var repairOrder = new RepairOrder(
                id: Guid.NewGuid(),
                workshopId: Guid.NewGuid(),
                cause: "Cause-test",
                details: "Detail-test",
                vehicle: _vehicle,
                recepcionDate: DateTime.Now,
                spareParts: new List<SparePart>(),
                deliveryDate: null
            );
            ;
            _repairOrderRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(repairOrder);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Equal(repairOrder, result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrow_WhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repairOrderRepositoryMock
                .Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync((RepairOrder)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.GetByIdAsync(id));
        }

        [Fact]
        public async Task GetByVehicleIdAsync_ReturnsHistoricalRepairOrders()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var limit = 2;

            var repairOrders = new List<RepairOrder>
            {
                new RepairOrder(
                    id: Guid.NewGuid(),
                    recepcionDate: DateTime.UtcNow.AddDays(-2),
                    cause: "Causa 1",
                    details: "Detalles 1",
                    workshopId: Guid.NewGuid(),
                    vehicle: new Vehicle(vehicleId, "ABC123", Guid.NewGuid()),
                    spareParts: new List<SparePart>(),
                    deliveryDate: DateTime.UtcNow.AddDays(-1)
                ),
                new RepairOrder(
                    id: Guid.NewGuid(),
                    recepcionDate: DateTime.UtcNow.AddDays(-1),
                    cause: "Causa 2",
                    details: "Detalles 2",
                    workshopId: Guid.NewGuid(),
                    vehicle: new Vehicle(vehicleId, "DEF456", Guid.NewGuid()),
                    spareParts: new List<SparePart>(),
                    deliveryDate: DateTime.UtcNow
                ),
            };

            _repairOrderRepositoryMock
                .Setup(r => r.GetByVehicleIdAsync(vehicleId))
                .ReturnsAsync(repairOrders);

            // Act
            var result = await _service.GetByVehicleIdAsync(vehicleId, limit);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, dto => Assert.IsType<HistoricalRepairOrderDto>(dto));
            Assert.Contains(result, dto => dto.Cause == "Causa 1");
            Assert.Contains(result, dto => dto.Cause == "Causa 2");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateRepairOrder()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new UpdateRepairOrderDto
            {
                Id = id.ToString(),
                ReceptionDate = DateTime.UtcNow,
                Budget = 100,
                Cause = "dto Test cause",
                Details = "dto Test details",
            };

            var repairOrder = new RepairOrder(
                id: Guid.NewGuid(),
                workshopId: Guid.NewGuid(),
                cause: "Cause-test",
                details: "Detail-test",
                vehicle: _vehicle,
                recepcionDate: DateTime.Now,
                spareParts: new List<SparePart>(),
                deliveryDate: null
            );

            _repairOrderRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(repairOrder);

            // Act
            await _service.UpdateAsync(dto);

            // Assert
            _repairOrderRepositoryMock.Verify(x => x.UpdateAsync(repairOrder), Times.Once);
            Assert.Equal(dto.Cause, repairOrder.Cause);
            Assert.Equal(dto.Budget, repairOrder.Budget);
            Assert.Equal(dto.Details, repairOrder.Details);
            Assert.Equal(dto.ReceptionDate, repairOrder.ReceptionDate);
        }

        [Fact]
        public async Task UpdateSpareParts_ShouldUpdateSpareParts()
        {
            // Arrange
            var id = Guid.NewGuid();
            List<UpdateSparePartDto> dto = new List<UpdateSparePartDto>()
            {
                new UpdateSparePartDto
                {
                    Name = "Filtro",
                    Price = 100,
                    Quantity = 1,
                },
                new UpdateSparePartDto
                {
                    Name = "Aceite",
                    Price = 1500,
                    Quantity = 1,
                },
            };
            var repairOrder = new RepairOrder(
                id: id,
                workshopId: Guid.NewGuid(),
                cause: "Cause-test",
                details: "Detail-test",
                vehicle: _vehicle,
                recepcionDate: DateTime.Now,
                spareParts: new List<SparePart>(),
                deliveryDate: null
            );

            _repairOrderRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(repairOrder);
            //_mapperMock
            //    .Setup(x => x.Map<List<SparePart>>(dto))
            //    .Returns(
            //        new List<SparePart>()
            //        {
            //            new SparePart
            //            {
            //                Name = "Filtro",
            //                Price = 100,
            //                Quantity = 1,
            //            },
            //            new SparePart
            //            {
            //                Name = "Aceite",
            //                Price = 1500,
            //                Quantity = 1,
            //            },
            //        }
            //    );
            // Act
            await _service.UpdateSpareParts(dto, id);

            // Assert
            _repairOrderRepositoryMock.Verify(x => x.UpdateAsync(repairOrder), Times.Once);
            Assert.Equal(dto.Count, repairOrder.SpareParts?.Count);
            Assert.Equal(dto[0].Name, repairOrder.SpareParts[0]?.Name);
            Assert.Equal(dto[0].Price, repairOrder.SpareParts[0]?.Price);
            Assert.Equal(dto[1].Name, repairOrder.SpareParts[1]?.Name);
            Assert.Equal(dto[1].Price, repairOrder.SpareParts[1]?.Price);
        }

        [Fact]
        public async Task UpdateStatusAndFinalAmount_ShouldUpdateStatusAndAmount()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new UpdateAmountAndStatusDto
            {
                Id = id,
                Status = "Completed",
                FinalAmount = 500,
            };

            var repairOrder = new RepairOrder(
                id: Guid.NewGuid(),
                workshopId: Guid.NewGuid(),
                cause: "Cause-test",
                details: "Detail-test",
                vehicle: _vehicle,
                recepcionDate: DateTime.Now,
                spareParts: new List<SparePart>(),
                deliveryDate: null
            );
            _repairOrderRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(repairOrder);

            // Act
            await _service.UpdateStatusAndFinalAmount(dto);

            // Assert
            _repairOrderRepositoryMock.Verify(x => x.UpdateAsync(repairOrder), Times.Once);
            Assert.Equal(dto.Status, repairOrder.Status.ToString());
            Assert.Equal(dto.FinalAmount, repairOrder.FinalAmount);
        }
    }
}
