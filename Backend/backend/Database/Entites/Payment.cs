namespace backend.Database.Entites
{
    public class Payment : BaseEntity<Guid>
    {
        public required Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string? Method { get; set; }
        public required decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}
