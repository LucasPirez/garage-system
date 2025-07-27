namespace backend.Modules.CustomerModule.Dtos
{
    public class UpdateCustomerDto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}
