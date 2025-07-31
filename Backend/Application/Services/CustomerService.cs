using Application.Dtos.Customer;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public interface ICustomerService
    {
        Task CreateAsync(CreateCustomerDto createDto);
        Task DeleteAsync(Guid Id);
        Task<IEnumerable<Customer>> GetAllAsync(Guid workshopId);
        Task<Customer> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid Id, UpdateCustomerDto customerDto);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateCustomerDto createDto)
        {
            Customer customer = _mapper.Map<Customer>(createDto);

            await _customerRepository.CreateAsync(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(Guid workshopId)
        {
            return await _customerRepository.GetAllAsync(workshopId);
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            Customer customer =
                await _customerRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException(id);

            return customer;
        }

        public async Task UpdateAsync(Guid Id, UpdateCustomerDto customerDto)
        {
            Customer customer =
                await _customerRepository.GetByIdAsync(Id) ?? throw new EntityNotFoundException(Id);

            customer.Update(
                firstName: customerDto.FirstName,
                lastName: customerDto.LastName,
                emails: customerDto.Email != null
                    ? new List<string>() { customerDto.Email }
                    : customer.Email,
                phoneNumbers: customerDto.PhoneNumber != null
                    ? new List<string>() { customerDto.PhoneNumber }
                    : customer.PhoneNumber
            );

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(Guid Id)
        {
            Customer customer =
                await _customerRepository.GetByIdAsync(Id) ?? throw new EntityNotFoundException(Id);

            await _customerRepository.DeleteAsync(customer);
        }
    }
}
