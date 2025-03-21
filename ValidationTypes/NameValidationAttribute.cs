using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HotelBackend.ValidationTypes
{
    public class NameValidationAttribute : ValidationAttribute
    {
        private static readonly Regex NameRegex = new(@"^[A-Za-zА-Яа-я\-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private const int MaxLength = 50;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return new ValidationResult("Поле является обязательным.");
                }

                if (!NameRegex.IsMatch(name))
                {
                    return new ValidationResult("Поле должно содержать только буквы (русские или латинские) и дефис.");
                }

                if (name.Length > MaxLength)
                {
                    return new ValidationResult($"Длина поля не может превышать {MaxLength} символов.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
