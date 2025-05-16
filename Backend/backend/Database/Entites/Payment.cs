namespace backend.Database.Entites
{
    public class Payment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string? Method { get; set; }
        public required decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}
