namespace Reclaim.Domain.Exceptions;

public class CustomValidationException : AppException
{
    public CustomValidationException(string message) : base(message)
    {
    }

    public CustomValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}