using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.AuthService.Api.DTOs
{
    public enum OtpPurpose
    {
        EmailVerfication=0,
        LoginVerification=1,
        PasswordReset=2,
        PasswordChange=3
    }
    public class AuthRequestVerificationDto
    {
        [Required]
        [RegularExpression(@"^\d{6}$")]
        public string Otp {  get; set; } = string.Empty;

        [Required]
        public Guid Userid { get; set; }
        [Required]
        public int OtpPurpose { get; set; }
    }
}
