using System.Net.Http.Json;
using backend.Database;
using backend.Modules.CustomerModule.Dtos;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace backend.Test.IntegrationTests
{
    public class CustomersTests : BaseIntegrationTest
    {
        private readonly HttpClient _client;

        public CustomersTests(IntegrationTestWebAppFactory factory, ITestOutputHelper output)
            : base(factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_Returns200AndList()
        {
            // Act
            var response = await _client.GetAsync($"workshops/{SeedData.workshopAId}/customers");

            // Assert
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("firstName", responseBody);
        }

        [Fact]
        public async Task GetById_Returns200AndCustomer()
        {
            // Act
            var response = await _client.GetAsync(
                $"workshops/{SeedData.workshopAId}/customers/{SeedData.customerAId}"
            );

            // Assert
            response.EnsureSuccessStatusCode();
            var customer = await response.Content.ReadFromJsonAsync<CreateCustomerDto>();
            Assert.NotNull(customer);
        }

        [Fact]
        public async Task Create_Returns201AndLocation()
        {
            // Arrange
            var dto = new CreateCustomerDto
            {
                FirstName = "Ana",
                LastName = "García",
                PhoneNumber = new List<string> { "3411234567" },
                Email = new List<string> { "ana@email.com" },
                Address = "Calle nueva 456",
                Dni = "87654321",
            };
            // Act
            var response = await _client.PostAsJsonAsync(
                $"workshops/{SeedData.workshopAId}/customers",
                dto
            );
            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            //Assert.NotNull(response.Headers.Location);
        }

        [Fact]
        public async Task Update_Returns204()
        {
            // Arrange
            var updateDto = new UpdateCustomerDto
            {
                FirstName = "Juan Actualizado",
                LastName = "Pérez",
                PhoneNumber = new List<string> { "3422548239" },
                Email = new List<string> { "juan@email.com" },
            };

            // Act
            var response = await _client.PatchAsJsonAsync(
                $"workshops/{SeedData.workshopAId}/customers/{SeedData.customerBId}",
                updateDto
            );

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);

            var DbContextCustomer = await DbContext.Customers.FirstOrDefaultAsync(K =>
                K.Id == Guid.Parse(SeedData.customerBId)
            );

            Assert.NotNull(DbContextCustomer);
            Assert.Equal(updateDto.FirstName, DbContextCustomer.FirstName);
        }

        //[Fact]
        //public async Task Delete_Returns204()
        //{
        //    var response = await _client.DeleteAsync(
        //        $"workshops/{SeedData.workshopAId}/customers/{SeedData.customerAId}"
        //    );
        //    Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        //}
    }
}
