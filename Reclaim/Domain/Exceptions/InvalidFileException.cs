namespace Reclaim.Domain.Exceptions;

public class InvalidFileException : AppException
{
    public InvalidFileException(string message) : base(message)
    {
    }

    public InvalidFileException(string message, Exception innerException) : base(message, innerException)
    {
    }
}