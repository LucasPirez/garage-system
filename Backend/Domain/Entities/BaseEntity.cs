namespace Domain.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; }

        public BaseEntity(T id)
        {
            Id = id;
        }
    }
}
