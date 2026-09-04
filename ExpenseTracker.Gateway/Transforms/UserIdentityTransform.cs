using System.Security.Claims;
using Yarp.ReverseProxy.Transforms;

namespace ExpenseTracker.Gateway.Transforms
{
    public class UserIdentityTransform : RequestTransform
    {
        public override ValueTask ApplyAsync(RequestTransformContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    context.ProxyRequest.Headers.Remove("X-User-Id");
                    context.ProxyRequest.Headers.Add("X-User-Id", userId);
                }
            }

            return ValueTask.CompletedTask;
        }
    }
}
