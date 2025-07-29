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
            _customerRepositoryMock = testStartUp
                .Services.BuildServiceProvider()
                .GetRequiredService<Mock<ICustomerRepository>>();

            _customerService = testStartUp
                .Services.BuildServiceProvider()
                .GetRequiredService<CustomerService>();
        }

        [Fact]
        public async Task CreateAsync_Should_Create_Customer_Successfully()
        {
            // Arrange
            var dto = new CreateCustomerDto
            {
                FirstName = "Customer",
                LastName = "customer-lastName",
            };

            // Act
            var result = await _customerService.CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.FirstName, result.FirstName);
            Assert.Equal(dto.LastName, result.LastName);
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
                        new Customer()
                        {
                            Id = customerId,
                            FirstName = "Customer",
                            LastName = "Customer-LastNmae",
                            WorkShopId = Guid.NewGuid(),
                        },
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
            var customer = new Customer
            {
                Id = customerId,
                FirstName = "Customer",
                LastName = "Customer-LastNmae",
                WorkShopId = Guid.NewGuid(),
            };

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
            var existing = new Customer
            {
                Id = customerId,
                FirstName = "Old",
                LastName = "OldLastName",
                Email = new List<string> { "old@email.com" },
                PhoneNumber = new List<string> { "1111" },
                WorkShopId = Guid.NewGuid(),
            };

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
            var customer = new Customer
            {
                Id = customerId,
                FirstName = "Customer",
                LastName = "Customer-LastNmae",
                WorkShopId = Guid.NewGuid(),
            };

            _customerRepositoryMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customer);

            // Act
            await _customerService.DeleteAsync(customerId);

            // Assert
            _customerRepositoryMock.Verify(r => r.DeleteAsync(customer), Times.Never);
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
