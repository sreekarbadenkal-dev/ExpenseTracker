using ExpenseTracker.Gateway.Transforms;
using Yarp.ReverseProxy.Transforms;

namespace ExpenseTracker.Gateway.Extensions
{
    public static class ReverseProxyExtentions
    {
        public static IServiceCollection AddGatewayReverseProxy(this IServiceCollection services, IConfiguration config)
        {
            var identifyTransform = new UserIdentityTransform();

            services.AddReverseProxy()
                .LoadFromConfig(config.GetSection("ReverseProxy"))
                .AddTransforms(builderContext =>
                {
                    builderContext.AddRequestTransform(identifyTransform.ApplyAsync);
                });

            return services;
        }
    }
}
