using AutoMapper;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;

namespace backend.Modules.VehicleEntryModule
{
    public class VehicleEntryService : Repository<VehicleEntry>
    {
        public VehicleEntryService(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }
    }
}
