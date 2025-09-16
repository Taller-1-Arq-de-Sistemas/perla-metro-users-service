using System.ComponentModel.DataAnnotations;

namespace PerlaMetroUsersService.Validators.Auth
{
    public class LastNamesAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastNamesAttribute"/> class.
        /// </summary>
        public LastNamesAttribute() { }

        /// <summary>
        /// Validates if the provided value is a valid last names string.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>True if the last names are valid; otherwise, false.</returns>
        public override bool IsValid(object? value)
        {
            var lastNames = value?.ToString();
            if (string.IsNullOrWhiteSpace(lastNames)) return true;
            lastNames = lastNames.Trim();
            return !HasNumbers(lastNames) && !HasSpecialChars(lastNames) && HasTwoLastNames(lastNames);
        }

        /// <summary>
        /// Determines if the input string contains any numeric characters.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <returns>True if the string contains numeric characters; otherwise, false.</returns>
        public static bool HasNumbers(string input)
        {
            foreach (char c in input)
                if (char.IsDigit(c))
                    return true;
            return false;
        }

        /// <summary>
        /// Determines if the input string contains any special characters.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <returns>True if the string contains special characters; otherwise, false.</returns>
        public static bool HasSpecialChars(string input)
        {
            foreach (char c in input)
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                    return true;
            return false;
        }

        /// <summary>
        /// Determines if the input string consists of exactly two last names.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <returns>True if the string consists of exactly two last names; otherwise, false.</returns>
        public static bool HasTwoLastNames(string input)
        {
            var names = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return names.Length == 2;
        }
    }
}
