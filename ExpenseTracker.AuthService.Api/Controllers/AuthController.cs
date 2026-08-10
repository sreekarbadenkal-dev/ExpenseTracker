using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.AuthService.Api.DTOs;
using ExpenseTracker.AuthService.Api.Services;

namespace ExpenseTracker.AuthService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthenticationService authService) : ControllerBase
    {

        [HttpPost("registration")]
        public async Task<IActionResult> Register(AuthRequestDto authRequest)
        {
            var jwtToken = await authService.Registration(authRequest);

            Response.Cookies.Append("access_token", jwtToken, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict });

            return Ok(new { message = "Registration Successfull", code = 1 });
        }

        [HttpPost("registration/verifyOtp")]
        public async Task<IActionResult> VerifyOtp(AuthRequestVerificationDto authRequest)
        {
            if(await authService.VerifyEmail(authRequest)) return Ok(new { message = "Registration Successfull", code = 1 });
            
            else return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequestDto authRequest)
        {
            var jwtToken =  await authService.Login(authRequest);

            Response.Cookies.Append("access_token",jwtToken,new CookieOptions { HttpOnly = true,Secure=true,SameSite=SameSiteMode.Strict });

            return Ok(new { message = "Login successfull", code = 1 });
        }

    }
}
