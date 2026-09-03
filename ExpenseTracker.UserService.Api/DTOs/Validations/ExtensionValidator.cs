using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.UserService.Api.DTOs.Validations
{
    public class ExtensionValidator(string[] _allowedExtensions) : ValidationAttribute
    {
        private readonly string[] allowedExtensions = _allowedExtensions;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    var allowedext = string.Join(", ", allowedExtensions);
                    return new ValidationResult(ErrorMessage ?? $"Only {allowedext} files are allowed");
                }
            }

            return ValidationResult.Success;
        }
    }
}
