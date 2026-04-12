using Microsoft.AspNetCore.Mvc;

namespace AccountingService.Api.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Service = "AccountingService.Api",
            Status = "Healthy",
            TimestampUtc = DateTime.UtcNow
        });
    }
}