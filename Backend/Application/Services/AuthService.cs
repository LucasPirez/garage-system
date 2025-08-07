using Application.Dtos.Auth;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public interface IAuthService
    {
        Task ChangePassword(string token, string password);
        Task<Admin> GetAdminByEmailAsync(string email);
        Task<AuthResponseDto> LoginAsync(AuthRequestDto request);
        Task SendTokenResetPassword(string email, string baseLink, string token);
        Task<string> GenerateAndSaveResetPasswordToken(string email);
    }

    public class AuthService : IAuthService
    {
        private readonly INotificationService _notificationService;
        private readonly IAdminRepository _adminRepository;

        public AuthService(
            INotificationService notificationService,
            IAdminRepository adminRepository
        )
        {
            _notificationService = notificationService;
            _adminRepository = adminRepository;
        }

        public async Task<AuthResponseDto> LoginAsync(AuthRequestDto request)
        {
            var admin = await _adminRepository.GetByEmailAsync(request.Email);

            if (admin == null)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            if (admin.FailedAttempts >= 5)
            {
                throw new UnauthorizedException("The acount is blocked");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, admin.Password))
            {
                admin.RegisterFailedLogin();
                await _adminRepository.UpdateAsync(admin);
                throw new UnauthorizedException("Invalid email or password.");
            }

            admin.ResetFailedAttempts();
            await _adminRepository.UpdateAsync(admin);

            return new AuthResponseDto()
            {
                Email = admin.Email,
                Token = "dummy-token",
                WorkShop = admin.WorkShop,
            };
        }

        public async Task ChangePassword(string token, string password)
        {
            Admin? admin =
                await _adminRepository.GetByResetPasswordTokenAsync(token)
                ?? throw new EntityNotFoundException(
                    "Vuelve a enviar la peticion de cambio de contraseña"
                );

            string hashPassword = BCrypt.Net.BCrypt.HashPassword(password);

            admin.SetPassword(hashPassword);

            await _adminRepository.UpdateAsync(admin);
        }

        public async Task<string> GenerateAndSaveResetPasswordToken(string email)
        {
            Admin? admin =
                await _adminRepository.GetByEmailAsync(email)
                ?? throw new EntityNotFoundException("Email no encontrado");

            string token = Guid.NewGuid().ToString();

            admin.SetResetPasswordToken(token);
            await _adminRepository.UpdateAsync(admin);

            return token;
        }

        public async Task SendTokenResetPassword(string email, string baseLink, string token)
        {
            string text =
                "Haz click en el siguiente link para cambiar la contraseña: " + baseLink + token;

            await _notificationService.Notify(
                message: text,
                recipient: email,
                subject: "Cambiar contraseña"
            );
        }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            return await _adminRepository.GetByEmailAsync(email)
                ?? throw new EntityNotFoundException("Email no encontrado");
        }
    }
}
