using backend.Database.Entites;
using backend.Modules.VehicleModule.Dtos;

namespace backend.Modules.VehicleModule.Interfaces
{
    public interface IVehicleService : IServiceBase<Vehicle, CreateVehicleDto> { }
}
