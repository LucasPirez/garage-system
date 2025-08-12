namespace Infraestructure.DataModel
{
    public class EFCustomer : Base
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public IList<string> PhoneNumber { get; set; } = new List<string>();

        public IList<string> Email { get; set; } = new List<string>();

        public string? Address { get; set; }

        public string? Dni { get; set; }

        public ICollection<EFVehicle> Vehicle { get; set; } = new List<EFVehicle>();

        public required Guid WorkShopId { get; set; }

        public EFWorkShop WorkShop { get; set; }
    }
}
