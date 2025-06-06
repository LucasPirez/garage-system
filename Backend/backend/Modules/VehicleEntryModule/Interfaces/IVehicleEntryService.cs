using backend.Database.Entites;
using backend.Modules.VehicleEntryModule.Dtos;

namespace backend.Modules.VehicleEntryModule.Interfaces
{
    public interface IVehicleEntryService : IServiceBase<VehicleEntry, CreateJobDto>
    {
        new Task<IEnumerable<ListJobsDto>> GetAllAsync(Guid workShopId);

        Task UpdateAsync(UpdateJobDto updateDto);
    }
}
