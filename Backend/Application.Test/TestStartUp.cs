using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Application.Test
{
    public class TestStartUp
    {
        public IServiceCollection Services { get; } = new ServiceCollection();

        public TestStartUp()
        {
            var customerRepository = new Mock<ICustomerRepository>();
            Services.AddApplication();
            Services.AddSingleton(customerRepository);
            Services.AddSingleton<ICustomerRepository>(customerRepository.Object);
        }
    }

    [CollectionDefinition("UnitTest")]
    public class IntegrationTestCollection : ICollectionFixture<TestStartUp> { }
}
