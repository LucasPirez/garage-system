using AutoMapper;
using backend.Common.Exceptions;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;
using backend.Modules.AuthModule.Dtos;
using backend.Modules.NotificationModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace backend.Modules.AuthModule
{
    public class AuthService : Repository<Admin>
    {
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public AuthService(
            AppDbContext database,
            IMapper mapper,
            INotificationService notificationService,
            IConfiguration configuration,
            IDbContextFactory<AppDbContext> contextFactory
        )
            : base(database, mapper)
        {
            _notificationService = notificationService;
            _configuration = configuration;
            _contextFactory = contextFactory;
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
                WorkShop = admin.WorkShop,
            };
        }

        public async Task ChangePassword(string token, string password)
        {
            Admin? admin =
                await _dbSet.Where(admin => admin.ResetPasswordToken == token).FirstOrDefaultAsync()
                ?? throw new NotFoundException(
                    "Vuelve a enviar la peticion de cambio de contraseña"
                );

            admin.Password = BCrypt.Net.BCrypt.HashPassword(password);
            admin.ResetPasswordToken = null;
            admin.FailedAttempts = 0;

            await Update(admin);
        }

        public async Task SendTokenResetPassword(string email)
        {
            await using var context = _contextFactory.CreateDbContext();

            var admin =
                await context.Admins.FirstOrDefaultAsync(a => a.Email == email)
                ?? throw new NotFoundException("Email no encontrado");

            string token = Guid.NewGuid().ToString();

            admin.ResetPasswordToken = token;

            await context.SaveChangesAsync();
            string? link =
                _configuration.GetValue<string>("LinkChangePassword:Link")
                ?? throw new Exception("link env variable is null");

            string text =
                "Haz click en el siguiente link para cambiar la contraseña: "
                + link
                + "?token="
                + token;

            await _notificationService.Notify(
                message: text,
                recipient: admin.Email,
                subject: "Cambiar contraseña"
            );
        }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            return await _dbSet.AsNoTracking().Where(k => k.Email == email).FirstOrDefaultAsync()
                ?? throw new NotFoundException("Email no encontrado");
        }
    }
}
