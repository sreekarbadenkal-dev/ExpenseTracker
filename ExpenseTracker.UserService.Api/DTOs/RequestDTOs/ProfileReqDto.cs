using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.UserService.Api.DTOs.RequestDTOs
{
    public class ProfileReqDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MinLength(2)]
        public string UserName { get; set; } 

        public string? Description { get; set; }


        public IFormFile? ProfilePicture { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }
    }
}
