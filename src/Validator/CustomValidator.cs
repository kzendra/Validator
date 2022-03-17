using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ValidatorTest
{
    public class CustomValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var type = value.GetType();
            if (type.IsEnum)
            {
                return ValidateEnum(value, validationContext);
            }
            else
                return ValidationResult.Success;
        }

        public static ValidationResult ValidateEnum(object value, ValidationContext validationContext)
        {
            var type = value.GetType();
            if (type.GetCustomAttributes<FlagsAttribute>().Any())
            {
                //throw new NotImplementedException();
                var mask = 0UL;
                foreach (var v in Enum.GetValues(type))
                    mask |= Convert.ToUInt64(v);
                var i = Convert.ToUInt64(value);
                return (i & mask) == i ? ValidationResult.Success : new ValidationResult("Error");
            }
            else
            {
                if (!Enum.IsDefined(type, value))
                    return new ValidationResult($"The value {value} is not an option for {validationContext.DisplayName} objects.");
                else
                    return ValidationResult.Success;
            }
        }
    }
}
