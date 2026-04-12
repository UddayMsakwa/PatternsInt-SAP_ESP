using Microsoft.AspNetCore.Mvc;

namespace ExchangeProxyService.Api.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Service = "ExchangeProxyService.Api",
            Status = "Healthy",
            TimestampUtc = DateTime.UtcNow
        });
    }
}