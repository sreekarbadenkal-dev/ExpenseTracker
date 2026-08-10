using System.Security.Cryptography;
using System.Text;
using StackExchange.Redis;

namespace ExpenseTracker.AuthService.Api.Services
{
    public class OtpService(IConnectionMultiplexer redis):IOtpService
    {
        private readonly IDatabase _redis=redis.GetDatabase();

        public async Task<string> GenerateAndStoreAsync(Guid userId)
        {
            var generatedOtp = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

            var otpHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(generatedOtp)));

            var key = $"otp:email-verification:{userId}";

            await _redis.StringSetAsync(key, otpHash, TimeSpan.FromMinutes(5));

            return generatedOtp;
        }

        public async Task<bool> VerifyAsync(Guid userId,string userOtp)
        {
            var key = $"otp:email-verification:{userId}";

            var storedHash = await _redis.StringGetAsync(key);

            if (storedHash.IsNullOrEmpty) return false;

            var hashedUserOtp = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(userOtp)));

            if(storedHash!=hashedUserOtp) return false;

            await _redis.KeyDeleteAsync(key);

            return true;
        }
    }
}
