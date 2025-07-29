namespace Domain.Entities
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllAsync(Guid workshopId);
        Task<Vehicle> GetByIdAsync(Guid id);
        Task CreateAsync(Vehicle entity);
        Task UpdateAsync(Vehicle entity);
        Task DeleteAsync(Guid id);
    }

    public class Vehicle : BaseEntity<Guid>
    {
        public required string Plate { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int Year { get; set; }

        public required Guid CustomerId { get; set; }
    }
}
