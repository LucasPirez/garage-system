using Microsoft.AspNetCore.Http;

namespace backend.Common.Exceptions
{
    public class ServiceException : Exception
    {
        public int StatusCode { get; }
        public string ErrorMessage { get; }

        public ServiceException(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
