namespace backend.Database.Entites
{
    public class WorkShop : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
        public ICollection<RepairOrder> VehicleEntries { get; set; } = new List<RepairOrder>();
    }
}
