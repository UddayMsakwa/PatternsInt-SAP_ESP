using Microsoft.AspNetCore.Mvc;
using AccountingService.Api.Infrastructure;

namespace AccountingService.Api.Controllers;

[ApiController]
[Route("api/positions")]
public sealed class PositionsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(AccountingState.Positions);
    }

    [HttpGet("{accountNumber}")]
    public IActionResult GetByAccountNumber(string accountNumber)
    {
        var positions = AccountingState.Positions
            .Where(x => x.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(positions);
    }
}
