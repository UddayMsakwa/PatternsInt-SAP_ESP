using Microsoft.AspNetCore.Mvc;
using AccountingService.Api.Infrastructure;

namespace AccountingService.Api.Controllers;

[ApiController]
[Route("api/ledger")]
public sealed class LedgerController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(AccountingState.LedgerEntries);
    }

    [HttpGet("{accountNumber}")]
    public IActionResult GetByAccountNumber(string accountNumber)
    {
        var entries = AccountingState.LedgerEntries
            .Where(x => x.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToList();

        return Ok(entries);
    }
}
