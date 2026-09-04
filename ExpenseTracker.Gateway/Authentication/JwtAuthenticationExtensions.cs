using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ExpenseTracker.Gateway.Authentication
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddGatewayJwtAuthentication(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtkey = configuration["JwtCred:key"];

            if (jwtkey == null)
            {
                throw new InvalidOperationException("Jwt key is not set");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         if(context.Request.Cookies.TryGetValue("access_token",out var token))
                         {
                             context.Token = token;
                         }
                         return Task.CompletedTask;
                     }
                 };
                 options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey)),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero,
                 };
             });

            return services;
        }
    }
}
