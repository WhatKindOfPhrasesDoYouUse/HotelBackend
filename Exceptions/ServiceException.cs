using System;

namespace HotelBackend.Exceptions
{
    public class ServiceException : Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public ServiceException(ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ServiceException(ErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
