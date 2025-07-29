namespace Application.Dtos.Vehicle
{
    public class BaseVehicleDto
    {
        public required string Plate { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
    }

    public class CreateVehicleDto : BaseVehicleDto
    {
        public required string CustomerId { get; set; }
    }
}
