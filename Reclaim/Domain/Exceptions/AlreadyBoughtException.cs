namespace Reclaim.Domain.Exceptions;

public class AlreadyBoughtException : AppException
{
    public AlreadyBoughtException(string message) : base(message)
    {
    }
    
    public AlreadyBoughtException(string message, Exception innerException) : base(message, innerException)
    {
    }
}