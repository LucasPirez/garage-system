using backend.Database.Entites;

namespace backend.Modules.CustomerModule.Dtos
{
    public class CreateCustomerDto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public ICollection<string> PhoneNumber { get; set; } = new List<string>();

        public ICollection<string> Email { get; set; } = new List<string>();

        public string? Address { get; set; }

        public string? Dni { get; set; }

        public required string WorkShopId { get; set; }
    }
}
