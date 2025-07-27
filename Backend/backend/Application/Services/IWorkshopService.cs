using Domain;
using backend.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Application.Services
{
    public interface IWorkshopService
    {
        Task<IEnumerable<Workshop>> GetAllAsync();
        Task<Workshop> GetByIdAsync(Guid id);
        Task<Workshop> CreateAsync(CreateWorkshopDto dto);
        Task UpdateAsync(UpdateWorkshopDto dto);
        Task DeleteAsync(Guid id);
    }
}
