using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.UserService.Api.Controllers
{
    [Route("api/user/profile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetProfile([FromRoute] Guid guid)
        {
            return Ok();
        }

        //[HttpPost("{userId:guid}/profilepicture")]
        //public async Task<IActionResult> UploadProfilePicture()
    }
}
