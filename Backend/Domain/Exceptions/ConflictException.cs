namespace Domain.Exceptions
{
    public class ConflictException : DomainException
    {
        public ConflictException(string errorMessage)
            : base(errorMessage) { }
    }
}
