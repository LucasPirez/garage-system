using Microsoft.EntityFrameworkCore;

namespace Infraestructure.DataModel
{
    public enum EFRepairOrderStatus
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

        public EFRepairOrderStatus Status { get; set; } = EFRepairOrderStatus.InProgress;

        public string Cause { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;
        public double Budget { get; set; }
        public double FinalAmount { get; set; }

        public IList<EFSparePart> SpareParts { get; set; } = new List<EFSparePart>();

        public Guid VehicleId { get; set; }

        public EFVehicle? Vehicle { get; set; }

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
            List<EFSparePart> spareParts,
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
            SpareParts = spareParts ?? new List<EFSparePart>();
            WorkShopId = workshopId;
            VehicleId = vehicleId;
        }
    }

    [Owned]
    public class EFSparePart
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
