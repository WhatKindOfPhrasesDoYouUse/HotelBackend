using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBackend.ValidationTypes
{
    public class CheckYearOfConstructionAttribute : ValidationAttribute
    {
        private readonly int _minYear = 1300;
        private readonly int _maxYear = DateTime.Now.Year;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int year)
            {
                if (year < _minYear || year > _maxYear)
                {
                    return new ValidationResult($"Поле YearOfConstruction должно быть в интервале от {_minYear} до {_maxYear}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
