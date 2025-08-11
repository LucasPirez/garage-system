namespace Domain.Entities
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAllAsync();
        Task<Admin?> GetByIdAsync(Guid id);
        Task<Admin?> GetByEmailAsync(string email);
        Task<Admin?> GetByResetPasswordTokenAsync(string email);
        Task CreateAsync(Admin entity);
        Task UpdateAsync(Admin entity);
        Task DeleteAsync(Guid id);
    }

    public class Admin : BaseEntity<Guid>
    {
        public string Email { get; private set; }

        public string Password { get; private set; }

        public int FailedAttempts { get; private set; } = 0;

        public string? ResetPasswordToken { get; private set; }

        public bool IsLocked { get; private set; } = false;

        public WorkShop WorkShop { get; private set; }

        public Admin(Guid id, string email, string password, WorkShop workShop)
            : base(id)
        {
            Email = email;
            Password = password;
            WorkShop = workShop;
        }

        public void RegisterFailedLogin()
        {
            FailedAttempts++;
        }

        public void ResetFailedAttempts()
        {
            FailedAttempts = 0;
        }

        public void SetPassword(string hashedPassword)
        {
            Password = hashedPassword;
            FailedAttempts = 0;
            ResetPasswordToken = null;
        }

        public void SetResetPasswordToken(string token)
        {
            ResetPasswordToken = token;
        }
    }
}
