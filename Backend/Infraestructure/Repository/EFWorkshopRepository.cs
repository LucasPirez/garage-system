using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class EFWorkshopRepository : IWorkshopRepository
    {
        private readonly AppDbContext _context;
        public EFWorkshopRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Workshop>> GetAllAsync() =>
            await _context.Workshops.ToListAsync();

        public async Task<Workshop> GetByIdAsync(Guid id) =>
            await _context.Workshops.FindAsync(id);

        public async Task<Workshop> CreateAsync(Workshop entity)
        {
            _context.Workshops.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Workshop entity)
        {
            _context.Workshops.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Workshops.FindAsync(id);
            if (entity != null)
            {
                _context.Workshops.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
