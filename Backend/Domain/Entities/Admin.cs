namespace Domain.Entities
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
        public string Email { get; }

        public string Password { get; }

        public int FailedAttempts { get; } = 0;

        public string? ResetPasswordToken { get; }

        public bool IsLocked { get; } = false;

        public WorkShop WorkShop { get; }

        public Admin(Guid id, string email, string password, WorkShop workShop)
            : base(id)
        {
            Email = email;
            Password = password;
            WorkShop = workShop;
        }
    }
}
