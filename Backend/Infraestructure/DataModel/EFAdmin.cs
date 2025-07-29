namespace Infraestructure.DataModel
{
    public class EFAdmin : Base
    {
        public required string Email { get; set; }

        public required string Password { get; set; }

        public int FailedAttempts { get; set; } = 0;

        public string? ResetPasswordToken { get; set; }

        public bool IsLocked { get; set; } = false;

        public required Guid WorkShopId { get; set; }
        public EFWorkShop WorkShop { get; set; }
    }
}
