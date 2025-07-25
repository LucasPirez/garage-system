using backend.Database.Entites;
using backend.Modules.CustomerModule.Dtos;
using backend.Modules.RepairOrderModule.Dtos;
using backend.Modules.VehicleModule.Dtos;

namespace backend.Modules.RepairOrderModule.Interfaces
{
    public interface IRepairOrderService : IServiceBase<RepairOrder, CreateRepairOrderDto>
    {
        new Task<IEnumerable<ListRepairOrderDto>> GetAllAsync(Guid workShopId);

        Task UpdateAsync(UpdateRepairOrderDto updateDto);

        Task UpdateStatusAndFinalAmount(UpdateAmountAndStatusDto dto);

        Task UpdateSpareParts(List<UpdateSparePartDto> dto, Guid repairOrderId);

        Task AddRepairOrder(CreateRepairOrderWithVehicleDto dto);
        Task AddRepairOrder(CreateRepairOrderWithVehicleAndCustomerDto dto);
    }
}
