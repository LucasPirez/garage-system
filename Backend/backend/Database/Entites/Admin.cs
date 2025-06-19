namespace backend.Database.Entites
{
    public class Admin : BaseEntity<Guid>
    {
        public required string Email { get; set; }

        public required string Password { get; set; }

        public required Guid WorkShopId { get; set; }
        public WorkShop WorkShop { get; set; }
    }
}
