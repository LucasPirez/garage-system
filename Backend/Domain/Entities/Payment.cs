namespace Domain.Entities
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllAsync(Guid workshopId);
        Task<Payment> GetByIdAsync(Guid id);
        Task CreateAsync(Payment entity);
        Task UpdateAsync(Payment entity);
        Task DeleteAsync(Guid id);
    }

    public class Payment : BaseEntity<Guid>
    {
        public Customer Customer { get; }
        public string? Method { get; }
        public decimal Amount { get; }
        public DateTime PaymentDate { get; } = DateTime.UtcNow;

        public Payment(Guid id, Customer customer, decimal amount = 0, string? method = "")
            : base(id)
        {
            Customer = customer;
            Method = method;
            Amount = amount;
        }
    }
}
