using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Validators.Auth;

/// <summary>
/// Validates that a password meets complexity requirements:
/// </summary>
public class PasswordAttribute : ValidationAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordAttribute"/> class.
    /// </summary>
    public PasswordAttribute() { }

    /// <summary>
    /// Validates if the provided value is a valid password string.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>True if the password is valid; otherwise, false.</returns>
    public override bool IsValid(object? value)
    {
        var password = value?.ToString();
        if (string.IsNullOrWhiteSpace(password)) return true;
        password = password.Trim();
        return PassAllRules(password);
    }

    /// <summary>
    /// Determines if the password meets all required rules:
    /// at least one uppercase letter, one lowercase letter, one digit,
    /// one special character, and a minimum length of 8 characters.
    /// </summary>
    /// <param name="password">The password string to check.</param>
    /// <returns>True if the password meets all rules; otherwise, false.</returns>
    public static bool PassAllRules(string password)
    {
        bool hasUpper = false, hasLower = false, hasDigit = false, hasSpecial = false;
        foreach (char c in password)
        {
            if (char.IsUpper(c))
                hasUpper = true;
            else if (char.IsLower(c))
                hasLower = true;
            else if (char.IsDigit(c))
                hasDigit = true;
            else if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                hasSpecial = true;
        }
        return hasUpper && hasLower && hasDigit && hasSpecial && password.Length >= 8;
    }
}
