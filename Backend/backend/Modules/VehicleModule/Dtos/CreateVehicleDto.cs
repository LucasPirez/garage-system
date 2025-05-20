using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace backend.Modules.VehicleModule.Dtos
{
    public class CreateVehicleDto
    {
        public required string Plate { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }

        public required string CustomerId { get; set; }
    }
}
