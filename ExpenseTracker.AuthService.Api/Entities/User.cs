using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.AuthService.Api.Entities
{
    [Index(nameof(UserId), IsUnique=true)]
    [Index(nameof(Email), IsUnique=true)]
    public class User
    {
        public long Id { get; set; }

        public Guid UserId{ get; set; }

        [Required]
        [MaxLength(300)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime? EmailVerifiedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public DateTime? DeletedAt {  get; set; }

    }
}
