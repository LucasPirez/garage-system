using System.Text.Json.Serialization;

namespace backend.Modules.CustomerModule.Dtos
{
    public class CreateCustomerDto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public List<string> PhoneNumber { get; set; } = new List<string>();

        public List<string> Email { get; set; } = new List<string>();

        public string? Address { get; set; }

        public string? Dni { get; set; }

        public string WorkshopId { get; set; }
    }
}
