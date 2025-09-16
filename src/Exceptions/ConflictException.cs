namespace PerlaMetroUsersService.Exceptions;

/// <summary>
/// Exception thrown when a conflict occurs, such as a model conflict.
/// </summary>
public class ConflictException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictException"/> class.
    /// </summary>
    public ConflictException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ConflictException(string? message)
        : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The inner exception that is the cause of the current exception.</param>
    public ConflictException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
