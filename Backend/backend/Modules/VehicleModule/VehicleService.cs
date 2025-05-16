using AutoMapper;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;

namespace backend.Modules.VehicleModule
{
    public class VehicleService : Repository<Vehicle>
    {
        public VehicleService(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }
    }
}
