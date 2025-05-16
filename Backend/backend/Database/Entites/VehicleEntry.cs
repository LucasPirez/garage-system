namespace backend.Database.Entites
{
    public enum VehicleStatus
    {
        InProgress,
        Completed,
        Cancelled,
    }

    public class VehicleEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime ReceptionDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }

        public bool NotifycationSent { get; set; } = false;

        public VehicleStatus Status { get; set; } = VehicleStatus.InProgress;

        public string Cause { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;
        public double Presupuest { get; set; }
        public double FinalAmount { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string? VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; }

        public required string WorkShopId { get; set; }
        public WorkShop WorkShop { get; set; }
    }
}
