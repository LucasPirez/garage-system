using AutoMapper;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;
using backend.Modules.AuthModule.Dtos;
using Microsoft.EntityFrameworkCore;

namespace backend.Modules.AuthModule
{
    public class AuthService : Repository<Admin>
    {
        public AuthService(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public async Task<AuthResponseDto> LoginAsync(AuthRequestDto request)
        {
            var admin = await _dbSet
                .Where(k => k.Email == request.Email)
                .Include(u => u.WorkShop)
                .FirstOrDefaultAsync();

            if (admin == null || !BCrypt.Net.BCrypt.Verify(request.Password, admin.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return new AuthResponseDto()
            {
                Email = admin.Email,
                Token = "dummy-token",
                WorkShopId = admin.WorkShopId.ToString(),
                WorkShopName = admin.WorkShop.Name,
            };
        }
    }
}
