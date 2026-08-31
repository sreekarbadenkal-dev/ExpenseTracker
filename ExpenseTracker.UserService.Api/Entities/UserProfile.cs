using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.UserService.Api.Entities
{
    [Index(nameof(UserId), IsUnique = true)]
    public class UserProfile
    {
        public long Id { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;


        public string? Description { get; set; }

        public byte[]? ProfilePicture { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
