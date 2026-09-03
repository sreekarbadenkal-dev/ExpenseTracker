using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.UserService.Api.DTOs.Validations
{
    public class MaxFileSize(int maxfilesize) : ValidationAttribute
    {
        private readonly int _maxfilesize = maxfilesize;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxfilesize)
                {
                    var sizeInMb = _maxfilesize / (1024 * 1024);
                    return new ValidationResult(ErrorMessage ?? $"The maximum size of image is {sizeInMb} mb");
                }

            }

            return ValidationResult.Success;
        }
    }
}
