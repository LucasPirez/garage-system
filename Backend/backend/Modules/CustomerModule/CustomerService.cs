using AutoMapper;
using backend.Common.Exceptions;
using backend.Database;
using backend.Database.Entites;
using backend.Database.Repository;
using backend.Modules.CustomerModule.Dtos;
using backend.Modules.CustomerModule.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Modules.CustomerModule
{
    public class CustomerService : Repository<Customer>, ICustomerService
    {
        public CustomerService(AppDbContext database, IMapper mapper)
            : base(database, mapper) { }

        public Task<Customer> CreateAsync(CreateCustomerDto createDto)
        {
            return AddWithDto(createDto);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(Guid workshopId)
        {
            return await _dbSet
                .Where(k => k.WorkShopId == workshopId)
                .Include(k => k.Vehicle)
                .ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            var customer = await GetById(id);

            if (customer == null)
            {
                throw new NotFoundException("Customer not found");
            }

            return customer;
        }

        public async Task UpdateAsync(Customer entity)
        {
            await Update(entity);
        }

        public async Task UpdateAsync(Guid Id, UpdateCustomerDto customerDto)
        {
            Customer customer = await GetByIdAsync(Id);

            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.Email =
                customerDto.Email != null
                    ? new List<string>() { customerDto.Email }
                    : customer.Email;

            customer.PhoneNumber =
                customerDto.PhoneNumber != null
                    ? new List<string>() { customerDto.PhoneNumber }
                    : customer.PhoneNumber;

            await UpdateAsync(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
