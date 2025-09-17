using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Validators.Auth;

/// <summary>
/// Validates that an email address belongs to the "perlametro.cl" domain.
/// </summary>
public class PerlaMetroEmailAttribute : ValidationAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PerlaMetroEmailAttribute"/> class.
    /// </summary>
    public PerlaMetroEmailAttribute() { }

    /// <summary>
    /// Validates if the provided value is a valid email address within the "perlametro.cl" domain.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the email is valid and belongs to the specified domain; otherwise, false.</returns>
    public override bool IsValid(object? value)
    {
        if (value is not string email)
            return true;

        email = email.Trim().ToLowerInvariant();
        var isValidEmail = new EmailAddressAttribute().IsValid(email);
        if (!isValidEmail) return false;
        var emailDomain = email.Split('@')[1];
        return emailDomain.Equals("perlametro.cl", StringComparison.CurrentCultureIgnoreCase);
    }
}
