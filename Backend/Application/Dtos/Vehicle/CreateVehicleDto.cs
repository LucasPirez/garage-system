namespace Application.Dtos.Vehicle
{
    public class BaseVehicleDto
    {
        public required Guid Id { get; set; }
        public required string Plate { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
    }

    public class VehicleDto : BaseVehicleDto
    {
        public required string CustomerId { get; set; }
    }

    public class CreateVehicleDto : VehicleDto { }
}
