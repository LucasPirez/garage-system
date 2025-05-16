namespace backend.Database.Entites
{
    public class Vehicle
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Plate { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }

        public required string CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<VehicleEntry> VehicleEntries { get; set; } = new List<VehicleEntry>();
    }
}
