using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Validators.Auth
{
    public class LastNamesAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var lastNames = value?.ToString();
            if (string.IsNullOrWhiteSpace(lastNames)) return true;
            lastNames = lastNames.Trim();
            return !HasNumbers(lastNames) && !HasSpecialChars(lastNames) && HasTwoLastNames(lastNames);
        }

        public static bool HasNumbers(string input)
        {
            foreach (char c in input)
                if (char.IsDigit(c))
                    return true;
            return false;
        }

        public static bool HasSpecialChars(string input)
        {
            foreach (char c in input)
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                    return true;
            return false;
        }

        public static bool HasTwoLastNames(string input)
        {
            var names = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return names.Length == 2;
        }
    }
}
