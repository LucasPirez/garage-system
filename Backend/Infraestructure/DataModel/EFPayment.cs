namespace Infraestructure.DataModel
{
    public class EFPayment : Base
    {
        public required Guid CustomerId { get; set; }
        public EFCustomer Customer { get; set; }
        public string? Method { get; set; }
        public required decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}
