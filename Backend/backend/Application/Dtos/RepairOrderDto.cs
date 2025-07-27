using System;

namespace backend.Application.Dtos
{
    public class CreateRepairOrderDto
    {
        public Guid VehicleId { get; set; }
        public Guid WorkshopId { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
    public class UpdateRepairOrderDto : CreateRepairOrderDto
    {
        public Guid Id { get; set; }
    }
}
