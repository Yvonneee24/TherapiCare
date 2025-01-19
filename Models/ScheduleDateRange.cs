using System;
using System.ComponentModel.DataAnnotations;

public class ScheduleDateRange : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateValue)
        {
            var minDate = DateTime.UtcNow.AddDays(3);
            if (dateValue < minDate)
            {
                return new ValidationResult($"The date must be at least 3 days from today.");
            }
        }

        return ValidationResult.Success;
    }
}
