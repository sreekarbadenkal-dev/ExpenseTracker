using System.ComponentModel.DataAnnotations;
using ExpenseTracker.UserService.Api.DTOs.Validations;

namespace ExpenseTracker.UserService.Api.DTOs.RequestDTOs;

public class ProfilePictureDto
{
    [Required]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "Please select an image file to upload.")]
    [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5 MB.")]
    [ExtensionValidator([".jpg", ".jpeg", ".png", ".webp"], ErrorMessage = "Only .jpg, .jpeg, .png, and .webp images are allowed.")]
    public IFormFile File { get; set; } = null!;
}
