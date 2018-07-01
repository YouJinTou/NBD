using System;
using System.ComponentModel.DataAnnotations;

namespace NBD.SDK.Attributes
{
    public class FutureDateTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;

            if (date != null)
            {
                return date.Value <= DateTime.Now ?
                    new ValidationResult("Date must be in the future.") :
                    ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}
