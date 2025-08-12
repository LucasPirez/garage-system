namespace Application.Exceptions
{
    public class UnauthorizedException : ServiceException
    {
        public UnauthorizedException(string message)
            : base(message) { }
    }
}
