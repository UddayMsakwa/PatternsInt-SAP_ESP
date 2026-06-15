using Microsoft.AspNetCore.Mvc;
using AccountingService.Api.Domain;

namespace AccountingService.Api.Controllers;

[ApiController]
[Route("api/ledger")]
public sealed class LedgerController : ControllerBase
{
    private static readonly List<LedgerEntry> Entries = [];

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Entries);
    }
}