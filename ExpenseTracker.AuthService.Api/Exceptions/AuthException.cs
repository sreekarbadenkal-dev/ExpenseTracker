namespace ExpenseTracker.AuthService.Api.Exceptions
{
    public class AuthException(int statusCode,string message,int resultCode) : Exception(message)
    {
        public int StatusCode { get; }  = statusCode;   
        public int ResultCode { get; } = resultCode;
    }
}
