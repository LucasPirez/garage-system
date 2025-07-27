using System;

namespace backend.Application.Dtos
{
    public class CreateVehicleDto
    {
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Guid CustomerId { get; set; }
    }
    public class UpdateVehicleDto : CreateVehicleDto
    {
        public Guid Id { get; set; }
    }
}
