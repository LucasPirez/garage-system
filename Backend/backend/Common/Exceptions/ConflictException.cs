namespace backend.Common.Exceptions
{
    public class ConflictException : ServiceException
    {
        public ConflictException(string message)
            : base(StatusCodes.Status409Conflict, message) { }
    }
}
