using System.ComponentModel.DataAnnotations;

namespace WebAPI_Learn.Validators
{
    public class RollCheckValidatorAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var roll = (int?)value;
            if (roll > 50)
            {
                return new ValidationResult("Roll cant be more than 50");
            }
            return ValidationResult.Success;
        }
    }
}
