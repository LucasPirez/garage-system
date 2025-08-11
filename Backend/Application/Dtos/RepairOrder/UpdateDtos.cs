namespace Application.Dtos.RepairOrder
{
    public class UpdateAmountAndStatusDto
    {
        public required double FinalAmount { get; set; }

        public required string Status { get; set; }

        public required Guid Id { get; set; }
    }

    public class UpdateSparePartDto
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class UpdateRepairOrderDto
    {
        public string Id { get; set; }
        public required DateTime ReceptionDate { get; set; } = DateTime.UtcNow;

        public required string Cause { get; set; } = string.Empty;

        public required string? Details { get; set; } = string.Empty;
        public required double? Budget { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool NotificationSent { get; set; }

        public double FinalAmount { get; set; }

        //public required List<SparePart>? SpareParts { get; set; } = new List<SparePart>();
    }
}
