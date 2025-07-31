using AutoMapper;
using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class EFVehicleRepository : WriteRepository<Vehicle, EFVehicle>, IVehicleRepository
    {
        public EFVehicleRepository(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(Guid workshopId)
        {
            return await _context
                .Customers.Where(c => c.WorkShopId == workshopId)
                .Include(v => v.Vehicle)
                .SelectMany(v => v.Vehicle)
                .Select(k => _mapper.Map<Vehicle>(k))
                .ToListAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(v => v.Id == id)
                .Select(k => _mapper.Map<Vehicle>(k))
                .FirstOrDefaultAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
