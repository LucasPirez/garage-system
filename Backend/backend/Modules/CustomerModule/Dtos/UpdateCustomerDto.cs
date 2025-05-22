namespace backend.Modules.CustomerModule.Dtos
{
    public class UpdateCustomerDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public List<string>? PhoneNumber { get; set; } = new List<string>();

        public List<string>? Email { get; set; } = new List<string>();

        public string? Address { get; set; }

        public string? Dni { get; set; }
    }
}
