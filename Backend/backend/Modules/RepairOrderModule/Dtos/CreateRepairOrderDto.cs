using backend.Database.Entites;
using backend.Modules.CustomerModule.Dtos;
using backend.Modules.VehicleModule.Dtos;

namespace backend.Modules.RepairOrderModule.Dtos
{
    public class CreateRepairOrderDto
    {
        public required DateTime ReceptionDate { get; set; } = DateTime.UtcNow;

        public required string Cause { get; set; } = string.Empty;

        public string? Details { get; set; } = string.Empty;

        public required string VehicleId { get; set; }

        public string WorkshopId { get; set; }
    }

    public class CreateRepairOrderWithVehicleDto
    {
        public required DateTime ReceptionDate { get; set; } = DateTime.UtcNow;

        public required string Cause { get; set; } = string.Empty;

        public string? Details { get; set; } = string.Empty;

        public required string WorkshopId { get; set; }

        public required CreateVehicleDto VehicleDto { get; set; }
    }

    public class CreateRepairOrderWithVehicleAndCustomerDto
    {
        public required DateTime ReceptionDate { get; set; } = DateTime.UtcNow;

        public required string Cause { get; set; } = string.Empty;

        public string? Details { get; set; } = string.Empty;

        public required string WorkshopId { get; set; }

        public required BaseVehicleDto VehicleDto { get; set; }

        public required BaseCustomerDto CustomerDto { get; set; }
    }
}
