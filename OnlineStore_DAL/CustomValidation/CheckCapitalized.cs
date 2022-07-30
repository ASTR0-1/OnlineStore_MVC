using System.ComponentModel.DataAnnotations;

namespace OnlineStore_DAL.CustomValidation
{
    public class CheckCapitalized : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (char.IsUpper(value.ToString()[0]))
                return ValidationResult.Success;
            else
                return new ValidationResult("First letter must be capital");
        }
    }
}
