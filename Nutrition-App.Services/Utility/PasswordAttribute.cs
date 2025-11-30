using System.ComponentModel.DataAnnotations;

namespace Nutrition_App.Services.Utility
{
    public class PasswordAttribute : ValidationAttribute
    {
        public PasswordAttribute()
        {
            const string defaultErrorMessage = "Error with Password";
            ErrorMessage ??= defaultErrorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Actual values are accessible in UserServices, but it is not possible to import UserServices
            // All values below are default hard-coded values.
            bool requireUppercase = true;
            bool requireLowercase = true;
            bool requireDigit = true;
            bool requireNonAlphanumeric = true;
            int requiredLength = 6;
            int requiredUniqueChars = 1;
            string password = "";

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Password is required.");
            }
            else
            {
                password = value.ToString()!;
            }



            if (requireUppercase)
            {
                bool found = false;
                foreach (char c in password)
                {
                    if (Char.IsUpper(c))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) return new ValidationResult("Password must contain an uppercase letter.");
            }

            if (requireLowercase)
            {
                bool found = false;
                foreach (char c in password)
                {
                    if (Char.IsLower(c))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) return new ValidationResult("Password must contain a lowercase letter.");
            }

            if (requireDigit)
            {
                bool found = false;
                foreach (char c in password)
                {
                    if (Char.IsDigit(c))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) return new ValidationResult("Password must contain a number.");
            }

            if (requireNonAlphanumeric)
            {
                bool found = false;
                foreach (char c in password)
                {
                    if (!Char.IsLetterOrDigit(c))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) return new ValidationResult("Password must contain a non-alphanumeric character.");
            }

            if (password.Length < requiredLength)
            {
                   return new ValidationResult($"Password must be at least {requiredLength} characters long.");
            }

            if (requiredUniqueChars > 0)
            {
                int uniqueChars = password.ToCharArray().Distinct().Count();
                if (uniqueChars < requiredUniqueChars)
                {
                    return new ValidationResult($"Password must have at least {requiredUniqueChars} unique characters.");
                }
            }

            return ValidationResult.Success; // Will return success if all filters are passed
        }
    }
}
