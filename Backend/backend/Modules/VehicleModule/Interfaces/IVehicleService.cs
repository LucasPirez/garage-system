using backend.Database.Entites;
using backend.Modules.VehicleModule.Dtos;

namespace backend.Modules.VehicleModule.Interfaces
{
    public interface IVehicleService : IServiceBase<Vehicle, CreateVehicleDto>
    {
        Task UpdateAsync(Guid Id, UpdateVehicleDto vehicle);

        Task<List<HistoricalRepairOrderDto>> GetRepairOrderByVehicleIdAsync(Guid id, int limit);
    }
}
