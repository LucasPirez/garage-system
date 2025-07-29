using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class EFRepairOrderRepository
    {
        //private readonly AppDbContext _context;
        //public EFRepairOrderRepository(AppDbContext context) => _context = context;

        //public async Task<IEnumerable<RepairOrder>> GetAllAsync(Guid workshopId) =>
        //    await _context.RepairOrders.Where(r => r.WorkshopId == workshopId).ToListAsync();

        //public async Task<RepairOrder> GetByIdAsync(Guid id) =>
        //    await _context.RepairOrders.FindAsync(id);

        //public async Task<RepairOrder> CreateAsync(RepairOrder entity)
        //{
        //    _context.RepairOrders.Add(entity);
        //    await _context.SaveChangesAsync();
        //    return entity;
        //}

        //public async Task UpdateAsync(RepairOrder entity)
        //{
        //    _context.RepairOrders.Update(entity);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(Guid id)
        //{
        //    var entity = await _context.RepairOrders.FindAsync(id);
        //    if (entity != null)
        //    {
        //        _context.RepairOrders.Remove(entity);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
