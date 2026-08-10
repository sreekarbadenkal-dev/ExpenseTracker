namespace ExpenseTracker.AuthService.Api.Services
{
    public interface IOtpService
    {
        Task<string> GenerateAndStoreAsync(Guid userId);

        Task<bool> VerifyAsync(Guid userId, string otp);
    }
}
