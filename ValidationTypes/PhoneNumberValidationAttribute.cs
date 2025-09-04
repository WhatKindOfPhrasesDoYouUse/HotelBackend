using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HotelBackend.ValidationTypes
{
    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        private static readonly Regex PhoneRegex = new(@"^\+?[0-9]{10,12}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private const int MaxLength = 12;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string phoneNumber)
            {
                if (string.IsNullOrWhiteSpace(phoneNumber))
                {
                    return new ValidationResult("Поле является обязательным.");
                }

                if (!PhoneRegex.IsMatch(phoneNumber))
                {
                    return new ValidationResult("Неверный формат номера телефона. Допустимы только цифры и знак '+'.");
                }

                if (phoneNumber.Length > MaxLength)
                {
                    return new ValidationResult($"Длина номера телефона не может превышать {MaxLength} символов.");
                }
            }

            return ValidationResult.Success;
        }
    }
}