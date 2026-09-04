using ExpenseTracker.Gateway.Authentication;
using ExpenseTracker.Gateway.Authorization;
using ExpenseTracker.Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 1. Register Gateway services via extension methods
builder.Services.AddGatewayJwtAuthentication(builder.Configuration);
builder.Services.AddGatewayJwtAuthorization();
builder.Services.AddGatewayReverseProxy(builder.Configuration);

var app = builder.Build();

// 2. Configure middleware pipeline (order is critical!)
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();