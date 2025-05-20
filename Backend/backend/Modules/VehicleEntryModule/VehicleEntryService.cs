using AutoMapper;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;
using backend.Modules.VehicleEntryModule.Dtos;
using backend.Modules.VehicleEntryModule.Interfaces;

namespace backend.Modules.VehicleEntryModule
{
    public class VehicleEntryService : Repository<VehicleEntry>, IVehicleEntryService
    {
        public VehicleEntryService(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public async Task<VehicleEntry> CreateAsync(CreateVehicleEntryDto createDto)
        {
            return await AddWithDto(createDto);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VehicleEntry>> GetAllAsync(Guid workShopId)
        {
            return await GetAll(k => k.WorkShopId == workShopId);
        }

        public Task<VehicleEntry> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(VehicleEntry entity)
        {
            throw new NotImplementedException();
        }
    }
}
