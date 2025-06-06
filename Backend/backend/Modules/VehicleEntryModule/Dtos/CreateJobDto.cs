using backend.Database.Entites;

namespace backend.Modules.VehicleEntryModule.Dtos
{
    public class CreateJobDto
    {
        public required DateTime ReceptionDate { get; set; } = DateTime.UtcNow;

        public required string Cause { get; set; } = string.Empty;

        public required string? Details { get; set; } = string.Empty;
        public required double? Presupuest { get; set; }

        public required string VehicleId { get; set; }

        public string WorkshopId { get; set; }

        public required List<SparePart>? SpareParts { get; set; } = new List<SparePart>();
    }

    public class UpdateJobDto
    {
        public string Id { get; set; }
        public required DateTime ReceptionDate { get; set; } = DateTime.UtcNow;

        public required string Cause { get; set; } = string.Empty;

        public required string? Details { get; set; } = string.Empty;
        public required double? Presupuest { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool NotificationSent { get; set; }

        public double FinalAmount { get; set; }

        public required List<SparePart>? SpareParts { get; set; } = new List<SparePart>();
    }
}
