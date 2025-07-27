using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class EFVehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;
        public EFVehicleRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Vehicle>> GetAllAsync(Guid workshopId) =>
            await _context.Vehicles.Where(v => v.CustomerId == workshopId).ToListAsync();

        public async Task<Vehicle> GetByIdAsync(Guid id) =>
            await _context.Vehicles.FindAsync(id);

        public async Task<Vehicle> CreateAsync(Vehicle entity)
        {
            _context.Vehicles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Vehicle entity)
        {
            _context.Vehicles.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Vehicles.FindAsync(id);
            if (entity != null)
            {
                _context.Vehicles.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
