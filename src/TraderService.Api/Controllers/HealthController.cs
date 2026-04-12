using Microsoft.AspNetCore.Mvc;

namespace TraderService.Api.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Service = "TraderService.Api",
            Status = "Healthy",
            TimestampUtc = DateTime.UtcNow
        });
    }
}