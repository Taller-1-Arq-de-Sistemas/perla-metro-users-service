namespace PerlaMetroUsersService.Exceptions;

/// <summary>
/// Exception thrown when a duplicate entity is encountered, such as a duplicate email.
/// </summary>
public class DuplicateException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateException"/> class.
    /// </summary>
    public DuplicateException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public DuplicateException(string? message)
        : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The inner exception that is the cause of the current exception.</param>
    public DuplicateException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
