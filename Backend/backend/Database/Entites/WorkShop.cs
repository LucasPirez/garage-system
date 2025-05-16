namespace backend.Database.Entites
{
    public class WorkShop
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;

        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
        public ICollection<VehicleEntry> VehicleEntries { get; set; } = new List<VehicleEntry>();
    }
}
