using backend.Database.Entites;
using backend.Modules.RepairOrderModule.Dtos;

namespace backend.Modules.RepairOrderModule.Interfaces
{
    public interface IRepairOrderService : IServiceBase<RepairOrder, CreateRepairOrderDto>
    {
        new Task<IEnumerable<ListRepairOrderDto>> GetAllAsync(Guid workShopId);

        Task UpdateAsync(UpdateRepairOrderDto updateDto);
    }
}
