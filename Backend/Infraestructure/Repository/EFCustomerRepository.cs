using Application.Dtos.Customer;
using Application.Dtos.Vehicle;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Context;
using Infraestructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class EFCustomerRepository
        : WriteRepository<Customer, EFCustomer>,
            ICustomerRepository,
            ICustomerProjectionQuery
    {
        public EFCustomerRepository(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public async Task<IEnumerable<Customer>> GetAllAsync(Guid workshopId)
        {
            return await _dbSet
                .Where(k => k.WorkShopId == workshopId)
                .Select(k => _mapper.Map<Customer>(k))
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(k => k.Id == id)
                .Select(k => _mapper.Map<Customer>(k))
                .FirstOrDefaultAsync();
        }

        async Task<IEnumerable<CustomerWithVehiclesDto>> ICustomerProjectionQuery.GetAllAsync(
            Guid workshopId
        )
        {
            return await _dbSet
                .Where(k => k.WorkShopId == workshopId)
                .Include(k => k.Vehicle)
                .Select(k => new CustomerWithVehiclesDto()
                {
                    Id = k.Id,
                    FirstName = k.FirstName,
                    LastName = k.LastName,
                    Vehicles = k
                        .Vehicle.Select(v => new BaseVehicleDto
                        {
                            Id = v.Id,
                            Plate = v.Plate,
                            Color = v.Color,
                        })
                        .ToList(),
                })
                .ToListAsync();
        }
    }
}
