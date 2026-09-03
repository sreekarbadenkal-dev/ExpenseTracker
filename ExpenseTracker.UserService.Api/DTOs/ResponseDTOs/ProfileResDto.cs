namespace ExpenseTracker.UserService.Api.DTOs.ResponseDTOs
{
    public class ProfileResDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? DateOfBirth { get; set; }

    }
}
