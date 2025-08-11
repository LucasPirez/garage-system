namespace Infraestructure.DataModel
{
    public class Base
    {
        public required Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
