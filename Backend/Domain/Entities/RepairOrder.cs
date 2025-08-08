using System.Data;

namespace Domain.Entities
{
    public interface IRepairOrderRepository
    {
        Task<IEnumerable<RepairOrder>> GetAllAsync(Guid workshopId);
        Task<RepairOrder?> GetByIdAsync(Guid id);
        Task<IEnumerable<RepairOrder>> GetByVehicleIdAsync(Guid vehicleId);

        Task CreateAsync(RepairOrder entity);
        Task UpdateAsync(RepairOrder entity);
        Task DeleteAsync(Guid id);
    }

    public enum RepairOrderStatus
    {
        InProgress,
        Completed,
        Cancelled,
    }

    public class RepairOrder : BaseEntity<Guid>
    {
        public DateTime ReceptionDate { get; private set; }
        public DateTime? DeliveryDate { get; private set; }

        public bool NotifycationSent { get; private set; }

        public RepairOrderStatus Status { get; private set; }

        public string Cause { get; private set; }

        public string Details { get; private set; }
        public double Budget { get; private set; }
        public double FinalAmount { get; private set; }

        public IList<SparePart> SpareParts { get; private set; }

        public Vehicle Vehicle { get; private set; }
        public Customer? Customer { get; private set; }

        public Guid WorkShopId { get; private set; }

        public RepairOrder(
            Guid id,
            DateTime recepcionDate,
            string cause,
            string details,
            Guid workshopId,
            Vehicle vehicle,
            List<SparePart> spareParts,
            DateTime? deliveryDate,
            Customer? customer = null,
            double? budget = 0,
            double finalAmount = 0,
            RepairOrderStatus status = RepairOrderStatus.InProgress,
            bool notifycationSent = false
        )
            : base(id)
        {
            ReceptionDate = recepcionDate;
            DeliveryDate = deliveryDate;
            Cause = cause;
            Details = details;
            WorkShopId = workshopId;
            Vehicle = vehicle;
            Customer = customer;
            SpareParts = spareParts;
            Budget = budget ?? 0;
            FinalAmount = finalAmount;
            NotifycationSent = false;
            Status = status;
            NotifycationSent = notifycationSent;
        }

        public void Update(
            DateTime recepcionDate,
            string cause,
            string details,
            DateTime? deliveriDate,
            double? budget = 0,
            double finalAmount = 0,
            RepairOrderStatus status = RepairOrderStatus.InProgress,
            bool notifycationSent = false
        )
        {
            ReceptionDate = recepcionDate;
            DeliveryDate = deliveriDate;
            Cause = cause;
            Details = details;
            Budget = budget ?? 0;
            FinalAmount = finalAmount;
            NotifycationSent = false;
            DeliveryDate = deliveriDate;
            Status = status;
            NotifycationSent = notifycationSent;
        }

        public void UpdateSpareParts(List<SparePart> spareParts)
        {
            SpareParts = spareParts;
        }

        public void UpdateStatusAndFinalAmount(RepairOrderStatus status, double finalAmount)
        {
            Status = status;
            FinalAmount = finalAmount;
        }
    }

    public class SparePart
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
