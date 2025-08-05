using AutoMapper;
using Domain;
using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class EFRepairOrderRepository
        : WriteRepository<RepairOrder, EFRepairOrder>,
            IRepairOrderRepository
    {
        public EFRepairOrderRepository(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public async Task<IEnumerable<RepairOrder>> GetAllAsync(Guid workshopId)
        {
            List<EFRepairOrder> listJobs = await _dbSet
                .Where(k => k.WorkShopId == workshopId)
                .Include(k => k.SpareParts)
                .Include(k => k.Vehicle)
                .OrderByDescending(repairOrder => repairOrder.ReceptionDate)
                .ToListAsync();

            return _mapper.Map<List<RepairOrder>>(listJobs);
        }

        public async Task<RepairOrder?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(k => k.Id == id)
                .Include(k => k.SpareParts)
                .Include(k => k.Vehicle)
                .Include(k => k.Vehicle.Customer)
                .Select(k => _mapper.Map<RepairOrder>(k))
                .FirstOrDefaultAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
