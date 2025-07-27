namespace Domain
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAllAsync();
        Task<Admin> GetByIdAsync(Guid id);
        Task<Admin> GetByEmailAsync(string email);
        Task CreateAsync(Admin entity);
        Task UpdateAsync(Admin entity);
        Task DeleteAsync(Guid id);
    }

    public class Admin : BaseEntity<Guid>
    {
        public required string Email { get; set; }

        public required string Password { get; set; }

        public int FailedAttempts { get; set; } = 0;

        public string? ResetPasswordToken { get; set; }

        public bool IsLocked { get; set; } = false;

        public WorkShop WorkShop { get; set; }
    }
}
