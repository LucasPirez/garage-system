using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class EFNotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        public EFNotificationRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Notification>> GetAllAsync() =>
            await _context.Notifications.ToListAsync();

        public async Task<Notification> GetByIdAsync(Guid id) =>
            await _context.Notifications.FindAsync(id);

        public async Task<Notification> CreateAsync(Notification entity)
        {
            _context.Notifications.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Notification entity)
        {
            _context.Notifications.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Notifications.FindAsync(id);
            if (entity != null)
            {
                _context.Notifications.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
