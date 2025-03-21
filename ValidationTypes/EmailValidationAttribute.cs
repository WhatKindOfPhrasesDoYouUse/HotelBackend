using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HotelBackend.ValidationTypes
{
    public class EmailValidationAttribute : ValidationAttribute
    {
        private static readonly Regex EmailRegex = new(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string email)
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return new ValidationResult("Поле Email является обязательным.");
                }

                if (!EmailRegex.IsMatch(email))
                {
                    return new ValidationResult("Неверный формат email.");
                }

                if (email.Length > 100)
                {
                    return new ValidationResult("Длина email не может превышать 100 символов.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
