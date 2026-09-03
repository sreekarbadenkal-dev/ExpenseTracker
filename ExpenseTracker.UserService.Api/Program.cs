using ExpenseTracker.UserService.Api;
using ExpenseTracker.UserService.Api.Data;
using ExpenseTracker.UserService.Api.Services;
using ExpenseTracker.UserService.Api.Services.Media;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register the UserPreferenceService into the Dependency Injection container
builder.Services.AddScoped<UserPreferenceService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IMediaService, CloudinaryMediaService>();
builder.Services.AddScoped<UserProfileService>();
builder.Services.AddDbContext<UserServiceDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
