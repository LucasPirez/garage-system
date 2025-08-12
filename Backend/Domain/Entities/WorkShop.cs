namespace Domain.Entities
{
    public interface IWorkShopRepository
    {
        Task<IEnumerable<WorkShop>> GetAllAsync();
        Task<IEnumerable<WorkShop>> GetAllByAdminAsync(Guid adminId);
        Task<WorkShop?> GetByIdAsync(Guid id);
        Task CreateAsync(WorkShop entity);
        Task UpdateAsync(WorkShop entity);
        Task DeleteAsync(Guid id);
    }

    public class WorkShop : BaseEntity<Guid>
    {
        public string Name { get; } = string.Empty;

        public string? Address { get; }

        public string? PhoneNumber { get; }

        public string? Email { get; }

        public WorkShop(Guid id, string name, string? address, string? phoneNumber, string? email)
            : base(id)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
