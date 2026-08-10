namespace ExpenseTracker.AuthService.Api.Services
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(  string email,   string otp);
    }
}
