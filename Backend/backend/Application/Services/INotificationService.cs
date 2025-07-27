using Domain;
using backend.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Application.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<Notification> GetByIdAsync(Guid id);
        Task<Notification> CreateAsync(CreateNotificationDto dto);
        Task UpdateAsync(UpdateNotificationDto dto);
        Task DeleteAsync(Guid id);
    }
}
