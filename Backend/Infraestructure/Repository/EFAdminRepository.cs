using AutoMapper;
using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class EFAdminRepository : WriteRepository<Admin, EFAdmin>, IAdminRepository
    {
        public EFAdminRepository(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            List<EFAdmin> admins = await _dbSet.ToListAsync();

            return _mapper.Map<List<Admin>>(admins);
        }

        public async Task<Admin?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .Where(adm => adm.Email == email)
                .Select(adm => _mapper.Map<Admin>(adm))
                .FirstOrDefaultAsync();
        }

        public async Task<Admin?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(adm => adm.Id == id)
                .Select(adm => _mapper.Map<Admin>(adm))
                .FirstOrDefaultAsync();
        }

        public async Task<Admin?> GetByResetPasswordTokenAsync(string resetPasswordToken)
        {
            return await _dbSet
                .Where(adm => adm.ResetPasswordToken == resetPasswordToken)
                .Select(adm => _mapper.Map<Admin>(adm))
                .FirstOrDefaultAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
