using ExpenseTracker.AuthService.Api.Data;
using ExpenseTracker.AuthService.Api.DTOs;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.AuthService.Api.Exceptions;
using System.Security.Claims;
using ExpenseTracker.AuthService.Api.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenseTracker.AuthService.Api.Services
{
    public class AuthenticationService(AuthServiceDbContext _db,IConfiguration configuration,IOtpService otpService,IEmailService emailService)
    {

        public async Task<User?> FindUserByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public string CreateJwtToken(User userdata)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userdata.UserId.ToString()),
                new Claim(ClaimTypes.Email,userdata.Email)
            };

            var jwtkey = configuration["JwtCred:key"] ?? throw new InvalidOperationException("JWT key is not configured.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            int.TryParse(configuration["JwtCred:ExpiresIn"], out int expiersIn); 
            var token = new JwtSecurityToken(claims: claims,expires: DateTime.UtcNow.AddHours(expiersIn), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<string> Registration(AuthRequestDto req)
        {
            var tryuser = await FindUserByEmail(req.Email);
            if (tryuser!=null)
            {
                throw new AuthException(409, "Email already exsists", 0);
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(req.Password);

            var user = new User { UserId = Guid.NewGuid(), Email = req.Email, PasswordHash = passwordHash, PhoneNumber = req.PhoneNumber };

            var jwtToken = CreateJwtToken(user);

            _db.Users.Add(user);

            await _db.SaveChangesAsync();

            var otp = await otpService.GenerateAndStoreAsync(user.UserId);

            Console.WriteLine($"OTP for {user.Email}: {otp}");

            await emailService.SendVerificationEmailAsync(user.Email,otp);

            return jwtToken;


        }

        public async Task<string> Login(AuthRequestDto req)
        {
            var currentUser = await FindUserByEmail(req.Email);

            if (currentUser==null)
            {
                throw new AuthException(401, "Email does not exsists", 0);
            }

            if (!(BCrypt.Net.BCrypt.Verify(req.Password, currentUser.PasswordHash)))
            {
                throw new AuthException(401, "Password is incorrect", 0);
            }

            var currentUserToken = CreateJwtToken(currentUser);

            if (currentUserToken == null)
            {
                throw new AuthException(401, "Token wasnt created", 0);
            }

            return currentUserToken;

        } 

        public async Task<bool> VerifyEmail(AuthRequestVerificationDto authverifyreq)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user=>user.UserId==authverifyreq.Userid);

            if (user == null) throw new AuthException(404, "User does not exsist", 0);

            var isValidOtpRes = await otpService.VerifyAsync(user.UserId, authverifyreq.Otp);

            if (!(isValidOtpRes)) throw new AuthException(400, "Invalid or Expired Otp", 1);

            user.EmailVerifiedAt = DateTime.UtcNow;
            //user.LastUpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return true;

        }
    }
}
