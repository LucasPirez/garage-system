using AutoMapper;
using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.DataModel;

namespace Infraestructure.Repository
{
    public class EFVehicleRepository : WriteRepository<Vehicle, EFVehicle>, IVehicleRepository
    {
        public EFVehicleRepository(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vehicle>> GetAllAsync(Guid workshopId)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
