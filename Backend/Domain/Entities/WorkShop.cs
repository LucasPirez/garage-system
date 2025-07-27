namespace Domain
{
    public interface IWorkShopRepository
    {
        Task<IEnumerable<WorkShop>> GetAllAsync();
        Task<IEnumerable<WorkShop>> GetAllByAdminAsync(Guid adminId);
        Task<WorkShop> GetByIdAsync(Guid id);
        Task<WorkShop> CreateAsync(WorkShop entity);
        Task UpdateAsync(WorkShop entity);
        Task DeleteAsync(Guid id);
    }

    public class WorkShop : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}
