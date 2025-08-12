namespace Application.Dtos.Vehicle
{
    public class UpdateVehicleDto
    {
        public required string Plate { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
    }
}
