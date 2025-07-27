using Domain;
using backend.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Application.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllAsync(Guid workshopId);
        Task<Vehicle> GetByIdAsync(Guid id);
        Task<Vehicle> CreateAsync(CreateVehicleDto dto);
        Task UpdateAsync(UpdateVehicleDto dto);
        Task DeleteAsync(Guid id);
    }
}
