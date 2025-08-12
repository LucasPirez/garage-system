namespace Infraestructure.DataModel
{
    public class EFWorkShop : Base
    {
        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public ICollection<EFCustomer> Customers { get; set; } = new List<EFCustomer>();
        public ICollection<EFRepairOrder> VehicleEntries { get; set; } = new List<EFRepairOrder>();
    }
}
