namespace Domain.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(Guid id)
            : base($"Entity not found with id ${id}") { }

        public EntityNotFoundException(string parameter)
            : base($"Entity not found with {nameof(parameter)} :${parameter}") { }
    }
}
