using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Validation;

public class CustomDateValidationAttribute : ValidationAttribute
{
    private readonly bool _canBeNullable;

    public CustomDateValidationAttribute(bool canBeNullable = false)
    {
        _canBeNullable = canBeNullable;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            if (!_canBeNullable)
            {
                return new ValidationResult($"{validationContext.DisplayName} is required.");
            }

            return ValidationResult.Success; // If nullable, null is acceptable
        }

        if (value is string dateString)
        {
            // Regex pattern to match dates in the format "yyyy-MM-dd"
            if (!Regex.IsMatch(dateString, @"^(19|20)\d{2}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"))
            {
                return new ValidationResult($"The {validationContext.DisplayName} field must be in yyyy-MM-dd format.");
            }

            var isValidDateTime = DateTime.TryParse(dateString, out var requestDate);
            if (!isValidDateTime)
            {
                return new ValidationResult($"Error parsing date from {validationContext.DisplayName}.");
            }

            if (requestDate > DateTime.Today)
            {
                return new ValidationResult($"The {validationContext.DisplayName} cannot have a future date.");
            }
        }
        else
        {
            return new ValidationResult($"The {validationContext.DisplayName} must be a string.");
        }

        return ValidationResult.Success;
    }
}
