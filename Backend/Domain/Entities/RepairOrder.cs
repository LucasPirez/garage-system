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
        public DateTime ReceptionDate { get; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; }

        public bool NotifycationSent { get; } = false;

        public RepairOrderStatus Status { get; } = RepairOrderStatus.InProgress;

        public string Cause { get; } = string.Empty;

        public string Details { get; } = string.Empty;
        public double Budget { get; }
        public double FinalAmount { get; }

        public IList<SparePart> SpareParts { get; } = new List<SparePart>();

        public Vehicle Vehicle { get; }

        public Guid WorkShopId { get; }

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
            : base(id)
        {
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
