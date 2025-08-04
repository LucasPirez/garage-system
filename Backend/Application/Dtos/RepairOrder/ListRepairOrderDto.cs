using Application.Dtos.Customer;
using Application.Dtos.Vehicle;
using Domain.Entities;

namespace Application.Dtos.RepairOrder
{
    public class ListRepairOrderDto
    {
        public Guid Id { get; set; }
        public DateTime ReceptionDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }

        public bool NotificationSent { get; set; } = false;

        public string Status { get; set; } = RepairOrderStatus.InProgress.ToString();

        public string Cause { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;
        public double Budget { get; set; }
        public double FinalAmount { get; set; }

        public IList<SparePart> SpareParts { get; set; } = new List<SparePart>();

        public required BaseVehicleDto Vehicle { get; set; }

        public required BaseCustomerDto Client { get; set; }
    }
}
