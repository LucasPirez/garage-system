using AutoMapper;
using backend.Common.Exceptions;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;
using backend.Modules.AuthModule.Dtos;
using backend.Modules.NotificationModule;
using Microsoft.EntityFrameworkCore;

namespace backend.Modules.AuthModule
{
    public class AuthService : Repository<Admin>
    {
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;

        public AuthService(
            AppDbContext database,
            IMapper mapper,
            INotificationService notificationService,
            IConfiguration configuration)
            : base(database, mapper)
        {
            _notificationService = notificationService;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(AuthRequestDto request)
        {
            var admin = await _dbSet
                .Where(k => k.Email == request.Email)
                .Include(u => u.WorkShop)
                .FirstOrDefaultAsync();

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
                admin.FailedAttempts++;
                await Update(admin);
                throw new UnauthorizedException("Invalid email or password.");
            }

            admin.FailedAttempts = 0;
            await Update(admin);

            return new AuthResponseDto()
            {
                Email = admin.Email,
                Token = "dummy-token",
                WorkShop = admin.WorkShop
            };
        }

        public async Task ChangePassword(string token, string password)
        {
            Admin? admin = await _dbSet
                                    .Where(admin => admin.ResetPasswordToken == token)
                                    .FirstOrDefaultAsync() ??
                                    throw new NotFoundException("Admin to change password not found.");

            admin.Password = BCrypt.Net.BCrypt.HashPassword(password);
            admin.FailedAttempts = 0;

            await Update(admin);
        }

        public async Task SendTokenResetPassword(string email)
        {
            var admin = await _dbSet
                .Where(k => k.Email == email)
                .Include(u => u.WorkShop)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Invalid email");

            string token = Guid.NewGuid().ToString();

            admin.ResetPasswordToken = token;

            await Update(admin);
            string? link = _configuration.GetValue<string>("LinkChangePassword:Link") ??
                                throw new Exception("link env variable is null");

            string text = "Click next link to change password: " + link + "?token=" + token;

            await _notificationService.Notify(text, admin.Email);
        }


    }
}
