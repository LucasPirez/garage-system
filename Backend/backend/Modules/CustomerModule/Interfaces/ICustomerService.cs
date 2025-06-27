using backend.Database.Entites;
using backend.Modules.CustomerModule.Dtos;

namespace backend.Modules.CustomerModule.Interfaces
{
    public interface ICustomerService : IServiceBase<Customer, CreateCustomerDto>
    {
        Task UpdateAsync(Guid Id, UpdateCustomerDto customerDto);
    }
}
