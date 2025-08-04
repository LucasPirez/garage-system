using Application.Dtos.Customer;
using Application.Dtos.Vehicle;

namespace Application.Dtos.RepairOrder
{
    public class BaseCreateRepairOrderDto
    {
        public required Guid Id { get; set; }
        public required DateTime ReceptionDate { get; set; } = DateTime.UtcNow;
        public required string Cause { get; set; } = string.Empty;
        public string? Details { get; set; } = string.Empty;
        public required string WorkshopId { get; set; }
    }

    public class CreateRepairOrderDto : BaseCreateRepairOrderDto
    {
        public required BaseVehicleDto Vehicle { get; set; }
    }

    public class CreateRepairOrderWithVehicleDto : BaseCreateRepairOrderDto
    {
        public required CreateVehicleDto VehicleDto { get; set; }
    }

    public class CreateRepairOrderWithVehicleAndCustomerDto : BaseCreateRepairOrderDto
    {
        public required BaseVehicleDto VehicleDto { get; set; }

        public required BaseCustomerDto CustomerDto { get; set; }
    }
}
