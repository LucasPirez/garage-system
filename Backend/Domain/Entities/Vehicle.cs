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
        public string Plate { get; }
        public string Brand { get; }
        public string Model { get; }
        public int Year { get; }

        public Guid CustomerId { get; }

        public Vehicle(Guid id, string plate, string brand, string model, int year, Guid customerId)
            : base(id)
        {
            Plate = plate;
            Brand = brand;
            Model = model;
            Year = year;
            CustomerId = customerId;
        }
    }
}
