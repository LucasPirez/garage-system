using System.Collections.Generic;

namespace Domain.Entities
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync(Guid workshopId);

        Task<Customer?> GetByIdAsync(Guid id);

        Task CreateAsync(Customer entity);

        Task UpdateAsync(Customer entity);

        Task DeleteAsync(Customer entity);
    }

    public class Customer : BaseEntity<Guid>
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public IList<string> PhoneNumber { get; private set; }

        public IList<string> Email { get; private set; }

        public string? Address { get; private set; }

        public string? Dni { get; private set; }

        public Guid WorkShopId { get; private set; }

        public Customer(
            Guid id,
            string firstName,
            string lastName,
            Guid workshopId,
            IEnumerable<string>? phoneNumbers = default,
            IEnumerable<string>? emails = default,
            string? address = null,
            string? dni = null
        )
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumbers?.ToList() ?? new List<string>();
            Email = emails?.ToList() ?? new List<string>();
            Address = address;
            Dni = dni;
            WorkShopId = workshopId;
        }

        public void Update(
            string firstName,
            string lastName,
            IList<string> phoneNumbers,
            IList<string> emails
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Email = emails;
            PhoneNumber = phoneNumbers;
        }
    }
}
