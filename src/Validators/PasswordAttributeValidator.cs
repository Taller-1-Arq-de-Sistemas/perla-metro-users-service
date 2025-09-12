using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Validators.Auth
{
    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var password = value?.ToString();
            if (string.IsNullOrWhiteSpace(password)) return true;
            password = password.Trim();
            return PassAllRules(password);
        }

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
}
