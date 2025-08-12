using System;
using System.Threading.Tasks;
using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Infraestructure.Test
{
    [Collection("IntegrationTest")]
    public class EFAdminRepositoryTests : IAsyncLifetime
    {
        private readonly DbSet<EFAdmin> _dbSetAdmin;
        private readonly AppDbContext _context;
        private readonly IAdminRepository _repository;
        private static readonly Guid workshopId = Guid.NewGuid();

        private readonly EFWorkShop workshopInDb = new EFWorkShop
        {
            Id = workshopId,
            Name = "Taller Test",
            Address = "Calle Falsa 123",
            Email = "taller@test.com",
        };

        private readonly Admin adminTest;

        public EFAdminRepositoryTests(TestStartup testStartup)
        {
            var services = testStartup.Services.BuildServiceProvider();

            _context = services.GetRequiredService<AppDbContext>();
            _dbSetAdmin = _context.Set<EFAdmin>();
            _repository = services.GetRequiredService<IAdminRepository>();

            adminTest = new Admin(
                id: Guid.NewGuid(),
                email: "admin@test.com",
                password: BCrypt.Net.BCrypt.HashPassword("1234"),
                workShop: new WorkShop(
                    workshopInDb.Id,
                    workshopInDb.Name,
                    workshopInDb.Address,
                    null,
                    workshopInDb.Email
                )
            );
        }

        [Fact]
        public async Task CreateAdmin_Success()
        {
            // Act
            await _repository.CreateAsync(adminTest);
            var fetched = await _dbSetAdmin.FirstOrDefaultAsync(a => a.Id == adminTest.Id);

            // Assert
            Assert.NotNull(fetched);
            Assert.Equal("admin@test.com", fetched.Email);
        }

        [Fact]
        public async Task GetByIdAdmin_ShouldReturnAdmin()
        {
            // Arrange
            var efAdmin = new EFAdmin
            {
                Id = Guid.NewGuid(),
                Email = "findme@test.com",
                Password = BCrypt.Net.BCrypt.HashPassword("pass"),
                WorkShopId = workshopInDb.Id,
            };

            _dbSetAdmin.Add(efAdmin);
            await _context.SaveChangesAsync();

            // Act
            var admin = await _repository.GetByIdAsync(efAdmin.Id);

            // Assert
            Assert.NotNull(admin);
            Assert.Equal(efAdmin.Id, admin.Id);
            Assert.Equal(efAdmin.Email, admin.Email);
        }

        [Fact]
        public async Task GetByEmailAdmin_ShouldReturnAdmin()
        {
            // Arrange
            var efAdmin = new EFAdmin
            {
                Id = Guid.NewGuid(),
                Email = "byemail@test.com",
                Password = BCrypt.Net.BCrypt.HashPassword("pass"),
                WorkShopId = workshopInDb.Id,
            };

            _dbSetAdmin.Add(efAdmin);
            await _context.SaveChangesAsync();

            // Act
            var admin = await _repository.GetByEmailAsync(efAdmin.Email);

            // Assert
            Assert.NotNull(admin);
            Assert.Equal(efAdmin.Email, admin.Email);
        }

        [Fact]
        public async Task GetByIdAdmin_ShouldReturnNull_WhenIdNotFound()
        {
            // Act
            var admin = await _repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(admin);
        }

        [Fact]
        public async Task UpdateAdmin_Success()
        {
            // Arrange
            var efAdmin = new EFAdmin
            {
                Id = Guid.NewGuid(),
                Email = "update@test.com",
                Password = BCrypt.Net.BCrypt.HashPassword("oldpass"),
                WorkShopId = workshopInDb.Id,
            };

            _dbSetAdmin.Add(efAdmin);
            await _context.SaveChangesAsync();

            var adminToUpdate = new Admin(
                id: efAdmin.Id,
                email: "update@test.com",
                password: BCrypt.Net.BCrypt.HashPassword("newpass"),
                workShop: new WorkShop(
                    workshopInDb.Id,
                    workshopInDb.Name,
                    workshopInDb.Address,
                    null,
                    workshopInDb.Email
                )
            );

            // Act
            await _repository.UpdateAsync(adminToUpdate);

            // Assert
            var updated = await _dbSetAdmin.FirstOrDefaultAsync(a => a.Id == efAdmin.Id);
            Assert.NotNull(updated);
            Assert.True(BCrypt.Net.BCrypt.Verify("newpass", updated.Password));
        }

        public async Task InitializeAsync()
        {
            _context.WorkShops.Add(workshopInDb);
            await _context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await _context.ResetDatabase();
        }
    }
}
