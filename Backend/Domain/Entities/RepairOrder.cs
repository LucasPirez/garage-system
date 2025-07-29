namespace Domain.Entities
{
    public interface IRepairOrderRepository
    {
        Task<IEnumerable<RepairOrder>> GetAllAsync(Guid workshopId);
        Task<RepairOrder> GetByIdAsync(Guid id);
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
        public DateTime ReceptionDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }

        public bool NotifycationSent { get; set; } = false;

        public RepairOrderStatus Status { get; set; } = RepairOrderStatus.InProgress;

        public string Cause { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;
        public double Budget { get; set; }
        public double FinalAmount { get; set; }

        public IList<SparePart> SpareParts { get; set; } = new List<SparePart>();

        public Vehicle Vehicle { get; set; }

        public required Guid WorkShopId { get; set; }

        public RepairOrder() { }

        public RepairOrder(
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
            Vehicle vehicle
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
            Vehicle = vehicle;
        }
    }

    public class SparePart
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
