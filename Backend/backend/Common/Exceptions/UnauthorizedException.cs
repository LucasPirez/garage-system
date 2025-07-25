namespace backend.Common.Exceptions
{
    public class UnauthorizedException : ServiceException
    {
        public UnauthorizedException(string message)
            : base(StatusCodes.Status401Unauthorized, message) { }
    }
}