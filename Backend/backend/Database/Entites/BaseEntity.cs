using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Database.Entites
{
    public class BaseEntity<T>
    {
        public required T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
