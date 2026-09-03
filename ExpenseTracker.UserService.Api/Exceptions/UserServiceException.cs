namespace ExpenseTracker.UserService.Api.Exceptions;

public class UserServiceException(int statusCode, string message, int resultCode = 0) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
    public int ResultCode { get; } = resultCode;
}