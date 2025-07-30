using Infraestructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Test
{
    public class TestStartup
    {
        public IServiceCollection Services { get; } = new ServiceCollection();
        private readonly SqliteConnection _connection;
        private readonly AppDbContext _context;

        public TestStartup()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            Services.AddInfraestructure();

            Services.AddDbContext<AppDbContext>(options => options.UseSqlite(_connection));

            _context = Services.BuildServiceProvider().GetRequiredService<AppDbContext>();

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreatedAsync().Wait();
        }

        public void ResetDataBase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreatedAsync().Wait();
        }
    }

    [CollectionDefinition("IntegrationTest")]
    public class IntegrationTestCollection : ICollectionFixture<TestStartup> { }
}
