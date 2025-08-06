using System;
using System.Threading.Tasks;
using Application.Dtos.Auth;
using Application.Exceptions;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Moq;
using Xunit;

namespace Application.Test
{
    public class AuthServiceTest
    {
        private readonly Mock<INotificationService> _notificationServiceMock;
        private readonly Mock<IAdminRepository> _adminRepositoryMock;
        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            _notificationServiceMock = new Mock<INotificationService>();
            _adminRepositoryMock = new Mock<IAdminRepository>();
            _authService = new AuthService(
                _notificationServiceMock.Object,
                _adminRepositoryMock.Object
            );
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnAuthResponse_WhenCredentialsAreValid()
        {
            // Arrange
            var admin = new Admin(
                Guid.NewGuid(),
                "admin@mail.com",
                BCrypt.Net.BCrypt.HashPassword("1234"),
                new WorkShop(Guid.NewGuid(), "Taller", null, null, null)
            );
            var request = new AuthRequestDto { Email = admin.Email, Password = "1234" };
            _adminRepositoryMock.Setup(r => r.GetByEmailAsync(admin.Email)).ReturnsAsync(admin);

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(admin.Email, result.Email);
            Assert.Equal("dummy-token", result.Token);
            Assert.Equal(admin.WorkShop, result.WorkShop);
            _adminRepositoryMock.Verify(r => r.UpdateAsync(admin), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrow_WhenAdminNotFound()
        {
            // Arrange
            var request = new AuthRequestDto { Email = "notfound@mail.com", Password = "1234" };
            _adminRepositoryMock
                .Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync((Admin)null);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedException>(() => _authService.LoginAsync(request));
        }

        [Fact]
        public async Task LoginAsync_ShouldThrow_WhenAccountBlocked()
        {
            // Arrange
            var admin = new Admin(
                Guid.NewGuid(),
                "admin@mail.com",
                BCrypt.Net.BCrypt.HashPassword("1234"),
                new WorkShop(Guid.NewGuid(), "Taller", null, null, null)
            );

            for (var i = 0; i < 5; i++)
            {
                admin.RegisterFailedLogin();
            }

            var request = new AuthRequestDto { Email = admin.Email, Password = "1234" };
            _adminRepositoryMock.Setup(r => r.GetByEmailAsync(admin.Email)).ReturnsAsync(admin);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedException>(() => _authService.LoginAsync(request));
        }

        [Fact]
        public async Task LoginAsync_ShouldThrow_WhenPasswordInvalid()
        {
            // Arrange
            var admin = new Admin(
                Guid.NewGuid(),
                "admin@mail.com",
                BCrypt.Net.BCrypt.HashPassword("1234"),
                new WorkShop(Guid.NewGuid(), "Taller", null, null, null)
            );
            var request = new AuthRequestDto { Email = admin.Email, Password = "wrong" };
            _adminRepositoryMock.Setup(r => r.GetByEmailAsync(admin.Email)).ReturnsAsync(admin);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedException>(() => _authService.LoginAsync(request));
            _adminRepositoryMock.Verify(r => r.UpdateAsync(admin), Times.Once);
        }

        [Fact]
        public async Task ChangePassword_ShouldUpdatePassword_WhenTokenIsValid()
        {
            // Arrange
            var admin = new Admin(
                Guid.NewGuid(),
                "admin@mail.com",
                BCrypt.Net.BCrypt.HashPassword("old"),
                new WorkShop(Guid.NewGuid(), "Taller", null, null, null)
            );
            var token = "token";
            var newPassword = "newpass";
            _adminRepositoryMock
                .Setup(r => r.GetByResetPasswordTokenAsync(token))
                .ReturnsAsync(admin);
            _adminRepositoryMock.Setup(r => r.UpdateAsync(admin)).Returns(Task.CompletedTask);

            // Act
            await _authService.ChangePassword(token, newPassword);

            // Assert
            _adminRepositoryMock.Verify(r => r.UpdateAsync(admin), Times.Once);
            Assert.True(BCrypt.Net.BCrypt.Verify(newPassword, admin.Password));
        }

        [Fact]
        public async Task ChangePassword_ShouldThrow_WhenTokenInvalid()
        {
            // Arrange
            var token = "invalid";
            _adminRepositoryMock
                .Setup(r => r.GetByResetPasswordTokenAsync(token))
                .ReturnsAsync((Admin)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => _authService.ChangePassword(token, "newpass")
            );
        }

        [Fact]
        public async Task SendTokenResetPassword_ShouldSendNotification_WhenEmailExists()
        {
            // Arrange
            var admin = new Admin(
                Guid.NewGuid(),
                "admin@mail.com",
                BCrypt.Net.BCrypt.HashPassword("1234"),
                new WorkShop(Guid.NewGuid(), "Taller", null, null, null)
            );
            var email = admin.Email;
            var baseLink = "https://reset.com/?token=";
            _adminRepositoryMock.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(admin);
            _adminRepositoryMock.Setup(r => r.UpdateAsync(admin)).Returns(Task.CompletedTask);
            _notificationServiceMock
                .Setup(n => n.Notify(It.IsAny<string>(), email, "Cambiar contraseña"))
                .Returns(Task.CompletedTask);

            // Act
            await _authService.SendTokenResetPassword(email, baseLink);

            // Assert
            _adminRepositoryMock.Verify(r => r.UpdateAsync(admin), Times.Once);
            _notificationServiceMock.Verify(
                n =>
                    n.Notify(
                        It.Is<string>(msg => msg.Contains(baseLink)),
                        email,
                        "Cambiar contraseña"
                    ),
                Times.Once
            );
        }

        [Fact]
        public async Task SendTokenResetPassword_ShouldThrow_WhenEmailNotFound()
        {
            // Arrange
            var email = "notfound@mail.com";
            _adminRepositoryMock.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync((Admin)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => _authService.SendTokenResetPassword(email, "link")
            );
        }

        [Fact]
        public async Task GetAdminByEmailAsync_ShouldReturnAdmin_WhenExists()
        {
            // Arrange
            var admin = new Admin(
                Guid.NewGuid(),
                "admin@mail.com",
                BCrypt.Net.BCrypt.HashPassword("1234"),
                new WorkShop(Guid.NewGuid(), "Taller", null, null, null)
            );
            _adminRepositoryMock.Setup(r => r.GetByEmailAsync(admin.Email)).ReturnsAsync(admin);

            // Act
            var result = await _authService.GetAdminByEmailAsync(admin.Email);

            // Assert
            Assert.Equal(admin, result);
        }

        [Fact]
        public async Task GetAdminByEmailAsync_ShouldThrow_WhenNotFound()
        {
            // Arrange
            var email = "notfound@mail.com";
            _adminRepositoryMock.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync((Admin)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => _authService.GetAdminByEmailAsync(email)
            );
        }
    }
}
