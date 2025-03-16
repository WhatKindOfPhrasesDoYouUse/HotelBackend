using System.ComponentModel.DataAnnotations;

namespace HotelBackend.ValidationTypes
{
    public class CheckDateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateOnly dateOfBirth)
            {
                if (dateOfBirth > DateOnly.FromDateTime(DateTime.Today))
                {
                    return new ValidationResult("Дата рождения не может быть в будующем.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
