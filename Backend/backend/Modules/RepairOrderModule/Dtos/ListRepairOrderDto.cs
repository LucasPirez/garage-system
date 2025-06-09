using backend.Database.Entites;

namespace backend.Modules.RepairOrderModule.Dtos
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

        public required ListJobsVehicleDto Vehicle { get; set; }

        public required ListJobsClientDto Client { get; set; }
    }

    public class ListJobsVehicleDto
    {
        public Guid Id { get; set; }
        public string Plate { get; set; } = string.Empty;
        public string? Model { get; set; }
    }

    public class ListJobsClientDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public IList<string> PhoneNumber { get; set; } = new List<string>();
        public IList<string> Email { get; set; } = new List<string>();
    }
}
