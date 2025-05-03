namespace Reclaim.Domain.Exceptions;

public class InsertionConflictException : AppException
{
    public InsertionConflictException(string message) : base(message)
    {
    }

    public InsertionConflictException(string message, Exception innerException) : base(message, innerException)
    {
    }
}