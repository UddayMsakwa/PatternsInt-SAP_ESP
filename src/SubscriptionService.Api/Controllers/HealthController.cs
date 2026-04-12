using Microsoft.AspNetCore.Mvc;

namespace SubscriptionService.Api.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Service = "SubscriptionService.Api",
            Status = "Healthy",
            TimestampUtc = DateTime.UtcNow
        });
    }
}