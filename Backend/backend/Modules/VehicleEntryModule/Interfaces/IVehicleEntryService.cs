using backend.Database.Entites;
using backend.Modules.VehicleEntryModule.Dtos;
using backend.Modules.VehicleModule.Dtos;

namespace backend.Modules.VehicleEntryModule.Interfaces
{
    public interface IVehicleEntryService : IServiceBase<VehicleEntry, CreateVehicleEntryDto> { }
}
