using ExpenseTracker.AuthService.Api.Exceptions;

namespace ExpenseTracker.AuthService.Api.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await next(ctx);
            }
            catch (AuthException ex)
            {
                logger.LogWarning(ex, "Auth error occured");
                ctx.Response.StatusCode = ex.StatusCode;
                await ctx.Response.WriteAsJsonAsync(new { message = ex.Message, code = ex.ResultCode });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Server error occured");
                ctx.Response.StatusCode = 500;
                await ctx.Response.WriteAsJsonAsync(new { message = "Some error occured", code = 0 });
            }
        }
    }
}
