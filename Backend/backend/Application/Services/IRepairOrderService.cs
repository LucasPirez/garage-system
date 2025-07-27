using Domain;
using backend.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Application.Services
{
    public interface IRepairOrderService
    {
        Task<IEnumerable<RepairOrder>> GetAllAsync(Guid workshopId);
        Task<RepairOrder> GetByIdAsync(Guid id);
        Task<RepairOrder> CreateAsync(CreateRepairOrderDto dto);
        Task UpdateAsync(UpdateRepairOrderDto dto);
        Task DeleteAsync(Guid id);
    }
}
