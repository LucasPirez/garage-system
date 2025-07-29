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
        public required Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string? Method { get; set; }
        public required decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}
