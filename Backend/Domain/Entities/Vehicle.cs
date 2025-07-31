namespace Domain.Entities
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllAsync(Guid workshopId);
        Task<Vehicle?> GetByIdAsync(Guid id);
        Task CreateAsync(Vehicle entity);
        Task UpdateAsync(Vehicle entity);
        Task DeleteAsync(Vehicle entity);
    }

    public class Vehicle : BaseEntity<Guid>
    {
        public string Plate { get; private set; }
        public string? Model { get; private set; }
        public string? Color { get; private set; }

        public Guid CustomerId { get; private set; }

        public Vehicle(Guid id, string plate, string model, string color, Guid customerId)
            : base(id)
        {
            Plate = plate;
            Model = model;
            Color = color;
            CustomerId = customerId;
        }

        public void Update(string plate, string? color, string? model)
        {
            Plate = plate;
            Color = color;
            Model = model;
        }
    }
}
