namespace backend.Common.Exceptions
{
    public class NotFoundException : ServiceException
    {
        public NotFoundException(string message)
            : base(StatusCodes.Status404NotFound, message) { }
    }
}
