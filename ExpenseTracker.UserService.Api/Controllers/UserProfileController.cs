using ExpenseTracker.UserService.Api.DTOs.RequestDTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.UserService.Api.Controllers
{
    [Route("api/users/profile")]
    [ApiController]
    public class UserProfileController(UserProfileService profileService) : ControllerBase
    {
        private readonly UserProfileService _profileService = profileService;
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetProfile([FromRoute] Guid userId)
        {
            var result =  await _profileService.getProfile(userId);
            if(result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("picture")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] ProfilePictureDto req)
        {
            var result = await _profileService.UploadProfilePicture(req);

            return Ok(result);
        }

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProfile([FromForm] ProfileReqDto profileReq)
        {
            var result = await  _profileService.CreateProfile(profileReq);

            if(result)
                return Ok(result);
            return BadRequest();

        }
    }
}
