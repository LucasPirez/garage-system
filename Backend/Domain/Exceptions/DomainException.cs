namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        public string ErrorMessage { get; }

        public DomainException(string errorMessage)
            : base(errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
