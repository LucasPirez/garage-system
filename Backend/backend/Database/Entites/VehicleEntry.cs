namespace backend.Database.Entites
{
    public enum VehicleStatus
    {
        InProgress,
        Completed,
        Cancelled,
    }

    public class VehicleEntry : BaseEntity<Guid>
    {
        public DateTime ReceptionDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }

        public bool NotifycationSent { get; set; } = false;

        public VehicleStatus Status { get; set; } = VehicleStatus.InProgress;

        public string Cause { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;
        public double Presupuest { get; set; }
        public double FinalAmount { get; set; }

        public IList<string> SpareParts { get; set; } = new List<string>();

        public Guid VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        public required Guid WorkShopId { get; set; }
        public WorkShop WorkShop { get; set; }
    }
}
