using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Infraestructure.DataModel
{
    public enum RepairOrderStatus
    {
        InProgress,
        Completed,
        Cancelled,
    }

    public class EFRepairOrder : Base
    {
        public DateTime ReceptionDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }

        public bool NotifycationSent { get; set; } = false;

        public RepairOrderStatus Status { get; set; } = RepairOrderStatus.InProgress;

        public string Cause { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;
        public double Budget { get; set; }
        public double FinalAmount { get; set; }

        public IList<SparePart> SpareParts { get; set; } = new List<SparePart>();

        public Guid VehicleId { get; set; }

        public EFVehicle Vehicle { get; set; }

        public required Guid WorkShopId { get; set; }
        public EFWorkShop WorkShop { get; set; }

        public EFRepairOrder() { }

        public EFRepairOrder(
            Guid id,
            DateTime recepcionDate,
            DateTime deliveriDate,
            bool notificationSent,
            string cause,
            string details,
            double budget,
            double finalAmount,
            List<SparePart> spareParts,
            Guid workshopId,
            Guid vehicleId
        )
        {
            Id = id;
            ReceptionDate = recepcionDate;
            DeliveryDate = deliveriDate;
            NotifycationSent = notificationSent;
            Cause = cause;
            Details = details;
            Budget = budget;
            FinalAmount = finalAmount;
            SpareParts = spareParts ?? new List<SparePart>();
            WorkShopId = workshopId;
            VehicleId = vehicleId;
        }
    }

    [Owned]
    public class SparePart
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
