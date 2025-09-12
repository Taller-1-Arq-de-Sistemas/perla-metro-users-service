using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Validators.Auth
{
    /// <summary>
    /// Validates an email address from Perla Metro.
    /// </summary>
    public class PerlaMetroEmailAttribute : ValidationAttribute
    {
        public PerlaMetroEmailAttribute() { }

        public PerlaMetroEmailAttribute(Func<string> errorMessageAccessor) : base(errorMessageAccessor) { }

        public PerlaMetroEmailAttribute(string errorMessage) : base(errorMessage) { }

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
}
