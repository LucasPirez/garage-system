using AutoMapper;
using backend.Common.Exceptions;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;
using backend.Modules.VehicleEntryModule.Dtos;
using backend.Modules.VehicleEntryModule.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Modules.VehicleEntryModule
{
    public class VehicleEntryService : Repository<VehicleEntry>, IVehicleEntryService
    {
        public VehicleEntryService(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public async Task<VehicleEntry> CreateAsync(CreateJobDto createDto)
        {
            return await AddWithDto(createDto);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ListJobsDto>> GetAllAsync(Guid workShopId)
        {
            IQueryable<ListJobsDto> listJobs = _dbSet
                .Where(k => k.WorkShopId == workShopId)
                .Include(k => k.SpareParts)
                .Include(k => k.Vehicle)
                .ThenInclude(k => k.Customer)
                .Select(k => new ListJobsDto()
                {
                    Id = k.Id,
                    Cause = k.Cause,
                    Details = k.Details,
                    NotificationSent = k.NotifycationSent,
                    budget = k.Presupuest,
                    ReceptionDate = k.ReceptionDate,
                    DeliveryDate = k.DeliveryDate,
                    Status = k.Status.ToString(),
                    SpareParts = k
                        .SpareParts.Select(sp => new SparePart()
                        {
                            Name = sp.Name,
                            Price = sp.Price,
                            Quantity = sp.Quantity,
                        })
                        .ToList(),
                    FinalAmount = k.FinalAmount,
                    Vehicle = new ListJobsVehicleDto()
                    {
                        Id = k.Vehicle.Id,
                        Plate = k.Vehicle.Plate,
                        Model = k.Vehicle.Model,
                    },
                    Client = new ListJobsClientDto()
                    {
                        Id = k.Vehicle.Customer.Id,
                        FirstName = k.Vehicle.Customer.FirstName,
                        LastName = k.Vehicle.Customer.LastName,
                        PhoneNumber = k.Vehicle.Customer.PhoneNumber,
                        Email = k.Vehicle.Customer.Email,
                    },
                });

            return await listJobs.ToListAsync();
        }

        public async Task<VehicleEntry> GetByIdAsync(Guid id)
        {
            var response = await GetById(id) ?? throw new NotFoundException("Vehicle not found");

            return response;
        }

        public async Task UpdateAsync(UpdateJobDto jobDto)
        {
            var entity =
                await GetByIdAsync(Guid.Parse(jobDto.Id))
                ?? throw new NotFoundException("Job not Found");

            entity.ReceptionDate = jobDto.ReceptionDate;
            entity.DeliveryDate = jobDto.DeliveryDate;
            entity.NotifycationSent = jobDto.NotificationSent;
            entity.Cause = jobDto.Cause;
            entity.Details = jobDto.Details ?? "";
            entity.Presupuest = jobDto.Presupuest ?? 0;
            entity.FinalAmount = jobDto.FinalAmount;
            entity.SpareParts = jobDto.SpareParts ?? new List<SparePart>();

            await Update(entity);
        }

        public Task UpdateAsync(VehicleEntry entity)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<VehicleEntry>> IServiceBase<VehicleEntry, CreateJobDto>.GetAllAsync(
            Guid workshopId
        )
        {
            throw new NotImplementedException();
        }
    }
}
