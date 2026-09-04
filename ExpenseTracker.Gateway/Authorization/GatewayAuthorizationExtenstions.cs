using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ExpenseTracker.Gateway.Authorization
{
    public static class GatewayAuthorizationExtenstions
    {
        public static IServiceCollection AddGatewayJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUserPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            return services;
        }
    }
}
