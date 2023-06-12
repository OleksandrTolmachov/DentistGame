using DentistBackend.WebApi.PlayerDbContext;
using System.ComponentModel.DataAnnotations;

namespace DentistBackend.WebApi.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class UniqueUsernameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string username)
        {
            bool isUsernameTaken = validationContext.GetRequiredService<GameDbContext>()
                .Users.Any(user => user.Username == username);

            if (isUsernameTaken)
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}
