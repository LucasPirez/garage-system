namespace backend.Database.Entites
{
    public class Vehicle : BaseEntity<Guid>
    {
        public required string Plate { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }

        public required Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<VehicleEntry> VehicleEntries { get; set; } = new List<VehicleEntry>();
    }

   
}
