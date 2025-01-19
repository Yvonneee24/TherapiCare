using System.ComponentModel.DataAnnotations;

namespace TherapiCareTest.Models
{
    public class ValidateDateRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;

            if (dt > DateTime.Now.ToUniversalTime())
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid Date");
            }
        }
    }
}
