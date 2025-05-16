namespace backend.Database.Entites
{
    public class Customer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public IList<string> PhoneNumber { get; set; } = new List<string>();

        public IList<string> Email { get; set; } = new List<string>();

        public string? Address { get; set; }

        public string? Dni { get; set; }

        public ICollection<Vehicle> Vehicle { get; set; } = new List<Vehicle>();

        public required string WorkShopId { get; set; }

        public WorkShop WorkShop { get; set; }
    }
}
