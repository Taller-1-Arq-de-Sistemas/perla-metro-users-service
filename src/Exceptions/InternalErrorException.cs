namespace PerlaMetroUsersService.Exceptions;

/// <summary>
/// Exception thrown when an internal server error occurs.
/// </summary>
public class InternalErrorException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InternalErrorException"/> class.
    /// </summary>
    public InternalErrorException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalErrorException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public InternalErrorException(string? message)
        : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalErrorException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The inner exception that is the cause of the current exception.</param>
    public InternalErrorException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
