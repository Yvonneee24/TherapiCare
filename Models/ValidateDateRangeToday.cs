using System;
using System.ComponentModel.DataAnnotations;

namespace TherapiCareTest.Models
{
    public class ValidateDateRangeToday : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Date is required.");
            }

            DateTime dt;
            bool isDate = DateTime.TryParse(value.ToString(), out dt);

            if (!isDate)
            {
                return new ValidationResult("Invalid date format.");
            }

            DateTime today = DateTime.UtcNow.Date;

            if (dt >= today)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date must be today or a future date.");
            }
        }
    }
}
