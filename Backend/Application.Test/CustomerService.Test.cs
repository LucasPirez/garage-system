using Application.Dtos.Customer;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Application.Test
{
    [Collection("UnitTest")]
    public class CustomerServiceTest
    {
        private readonly CustomerService _customerService;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public CustomerServiceTest(TestStartUp testStartUp)
        {
            var services = testStartUp.Services.BuildServiceProvider();

            _customerRepositoryMock = services.GetRequiredService<Mock<ICustomerRepository>>();

            _customerService = services.GetRequiredService<CustomerService>();
        }

        [Fact]
        public async Task CreateAsync_Should_Create_Customer_Successfully()
        {
            // Arrange
            var dto = new CreateCustomerDto
            {
                Id = Guid.NewGuid(),
                FirstName = "Customer",
                LastName = "customer-lastName",
                WorkshopId = Guid.NewGuid().ToString(),
            };

            // Act
            await _customerService.CreateAsync(dto);

            // Assert
            _customerRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_Customers()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var workshopId = Guid.NewGuid();
            _customerRepositoryMock
                .Setup(r => r.GetAllAsync(workshopId))
                .ReturnsAsync(
                    new List<Customer>
                    {
                        new Customer(
                            id: customerId,
                            firstName: "Customer",
                            lastName: "Customer-LastNmae",
                            workshopId: Guid.NewGuid()
                        ),
                    }
                );

            // Act
            var result = await _customerService.GetAllAsync(workshopId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Customer_When_Exists()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new Customer(
                id: customerId,
                firstName: "Customer",
                lastName: "Customer-LastNmae",
                workshopId: Guid.NewGuid()
            );

            _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetByIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Throw_When_NotFound()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            _customerRepositoryMock
                .Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync((Customer)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => _customerService.GetByIdAsync(customerId)
            );
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Customer_Successfully()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var existing = new Customer(
                id: customerId,
                firstName: "Old",
                lastName: "OldLastName",
                emails: new List<string> { "old@email.com" },
                phoneNumbers: new List<string> { "1111" },
                workshopId: Guid.NewGuid()
            );

            var dto = new UpdateCustomerDto
            {
                FirstName = "New",
                LastName = "NewLastName",
                Email = "new@email.com",
                PhoneNumber = "2222",
            };

            _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(existing);

            // Act
            await _customerService.UpdateAsync(customerId, dto);

            // Assert
            Assert.Equal("New", existing.FirstName);
            Assert.Equal("NewLastName", existing.LastName);
            Assert.Equal("new@email.com", existing.Email[0]);
            _customerRepositoryMock.Verify(r => r.UpdateAsync(existing), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_When_NotFound()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var dto = new UpdateCustomerDto { FirstName = "X", LastName = "Y" };

            _customerRepositoryMock
                .Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync((Customer)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => _customerService.UpdateAsync(customerId, dto)
            );
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete_Customer_When_Exists()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new Customer(
                id: customerId,
                firstName: "Customer",
                lastName: "Customer-LastNmae",
                workshopId: Guid.NewGuid()
            );

            _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);

            // Act
            await _customerService.DeleteAsync(customerId);

            // Assert
            _customerRepositoryMock.Verify(r => r.DeleteAsync(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Throw_When_NotFound()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            _customerRepositoryMock
                .Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync((Customer)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => _customerService.DeleteAsync(customerId)
            );
        }
    }
}
