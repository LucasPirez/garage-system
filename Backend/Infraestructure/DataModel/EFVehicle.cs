using System.Text.Json.Serialization;

namespace Infraestructure.DataModel
{
    public class EFVehicle : Base
    {
        public required string Plate { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }

        public required Guid CustomerId { get; set; }

        [JsonIgnore]
        public EFCustomer Customer { get; set; }

        public ICollection<EFRepairOrder> VehicleEntries { get; set; } = new List<EFRepairOrder>();
    }
}
