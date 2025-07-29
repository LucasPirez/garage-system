namespace Domain.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(Guid id)
            : base($"Entity not found with id ${id}") { }
    }
}
