using AutoMapper;
using backend.Common.Exceptions;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;
using backend.Modules.VehicleModule.Dtos;
using backend.Modules.VehicleModule.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Modules.VehicleModule
{
    public class VehicleService : Repository<Vehicle>, IVehicleService
    {
        public VehicleService(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public async Task<Vehicle> CreateAsync(CreateVehicleDto createDto)
        {
            return await AddWithDto(createDto);
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(Guid workshopId)
        {
            return await _context
                .Customers.Where(k => k.WorkShopId == workshopId)
                .Include(k => k.Vehicle)
                .SelectMany(k => k.Vehicle)
                .ToListAsync();
        }

        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            var vehicle = await GetById(id);

            if (vehicle == null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            return vehicle;
        }

        public Task UpdateAsync(Vehicle entity)
        {
            return Update(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Guid Id, UpdateVehicleDto vehicleDto)
        {
            Vehicle vehicle = await GetByIdAsync(Id);

            vehicle.Plate = vehicleDto.Plate;
            vehicle.Color = vehicleDto.Color;
            vehicle.Model = vehicleDto.Model;

            await UpdateAsync(vehicle);
        }

        public async Task<List<HistoricalRepairOrderDto>> GetRepairOrderByVehicleIdAsync(
            Guid Id,
            int limit
        )
        {
            return await _context
                .VehicleEntries.Where(repairOrder => repairOrder.VehicleId == Id)
                .Select(RepairOrder => new HistoricalRepairOrderDto()
                {
                    Id = RepairOrder.Id.ToString(),
                    Budget = RepairOrder.Budget,
                    Cause = RepairOrder.Cause,
                    Details = RepairOrder.Details,
                    DeliveryDate = RepairOrder.DeliveryDate,
                    FinalAmount = RepairOrder.FinalAmount,
                    NotifycationSent = RepairOrder.NotifycationSent,
                    ReceptionDate = RepairOrder.ReceptionDate,
                    SpareParts = RepairOrder
                        .SpareParts.Select(sparePart => new SparePart
                        {
                            Name = sparePart.Name,
                            Price = sparePart.Price,
                            Quantity = sparePart.Quantity,
                        })
                        .ToList(),
                    Status = RepairOrder.Status.ToString(),
                })
                .Take(limit)
                .OrderByDescending(repairOrder => repairOrder.ReceptionDate)
                .ToListAsync();
        }
    }
}
