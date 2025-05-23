using backend.Database.Entites;
using backend.Modules.VehicleEntryModule.Dtos;

namespace backend.Modules.VehicleEntryModule.Interfaces
{
    public interface IVehicleEntryService : IServiceBase<VehicleEntry, CreateVehicleEntryDto> {

        new
        Task<IEnumerable<ListJobsDto>> GetAllAsync(Guid workShopId);
    }
}
