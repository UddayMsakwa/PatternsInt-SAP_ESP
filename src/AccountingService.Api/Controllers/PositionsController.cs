using Microsoft.AspNetCore.Mvc;
using AccountingService.Api.Domain;

namespace AccountingService.Api.Controllers;

[ApiController]
[Route("api/positions")]
public sealed class PositionsController : ControllerBase
{
    private static readonly List<Position> Positions = [];

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Positions);
    }
}