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
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public IList<string> PhoneNumber { get; set; } = new List<string>();

        public IList<string> Email { get; set; } = new List<string>();

        public string? Address { get; set; }

        public string? Dni { get; set; }

        public required Guid WorkShopId { get; set; }
    }
}
