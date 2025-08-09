using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
                .Include(k => k.Vehicle.Customer)
                .OrderByDescending(repairOrder => repairOrder.ReceptionDate)
                .ToListAsync();

            return _mapper.Map<List<RepairOrder>>(listJobs);
        }

        public new async Task UpdateAsync(RepairOrder entity)
        {
            var existingEntity = await _dbSet
                .Include(e => e.SpareParts)
                .FirstOrDefaultAsync(e => e.Id == entity.Id);

            _mapper.Map(entity, existingEntity);

            await _context.SaveChangesAsync();
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

        public async Task<IEnumerable<RepairOrder>> GetByVehicleIdAsync(Guid vehicleId)
        {
            return await _dbSet
                .Where(k => k.VehicleId == vehicleId)
                .Include(k => k.SpareParts)
                .Select(k => _mapper.Map<RepairOrder>(k))
                .ToListAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
