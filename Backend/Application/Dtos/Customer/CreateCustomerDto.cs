using System.Text.Json.Serialization;

namespace Application.Dtos.Customer
{
    public class BaseCustomerDto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public List<string> PhoneNumber { get; set; } = new List<string>();

        public List<string> Email { get; set; } = new List<string>();

        public string? Address { get; set; }

        public string? Dni { get; set; }
    }

    public class CreateCustomerDto : BaseCustomerDto
    {
        public string WorkshopId { get; set; }
    }
}
