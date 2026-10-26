using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.DeviceConsentService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                service = "ExpenseTracker.DeviceConsentService.Api",
                status = "Healthy",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
