using ExpenseTracker.UserService.Api.DTOs.RequestDTOs;
using ExpenseTracker.UserService.Api.DTOs.ResponseDTOs;
using ExpenseTracker.UserService.Api.Exceptions;
using ExpenseTracker.UserService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.UserService.Api.Controllers
{
    [Route("api/users/preference")]
    [ApiController]
    public class UserPreferenceController(UserPreferenceService preferenceService) : ControllerBase
    {
        private readonly UserPreferenceService _preferenceService=preferenceService;

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetPreferences([FromRoute] Guid userId )
        {
            var result = await _preferenceService.GetPreferenceAsync(userId);

            if (result == null)
            {
                return NotFound( new { message = $"Preference user not found with id {userId}" });
            }

            return Ok(result);
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> PutPreferences([FromBody] PreferenceReqDto req,[FromRoute] Guid userId)
        {
            try
            {
                var result = await _preferenceService.UpdatePreferenceAsync(userId, req);
                return Ok(result);
            }
            catch (UserServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            
     
        }
    }
}
