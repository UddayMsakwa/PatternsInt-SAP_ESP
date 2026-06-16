using Microsoft.AspNetCore.Mvc;
using AccountingService.Api.Domain;
using AccountingService.Api.Infrastructure;

namespace AccountingService.Api.Controllers;

[ApiController]
[Route("api/accounts")]
public sealed class AccountsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(AccountingState.Accounts);
    }

    [HttpGet("{accountNumber}")]
    public IActionResult GetByAccountNumber(string accountNumber)
    {
        var account = AccountingState.Accounts
            .FirstOrDefault(x => x.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase));

        return account is null ? NotFound(new { Message = "Account not found." }) : Ok(account);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Account account)
    {
        if (string.IsNullOrWhiteSpace(account.AccountNumber))
        {
            return BadRequest(new { Message = "AccountNumber is required." });
        }

        account.Id = Guid.NewGuid();
        AccountingState.Accounts.Add(account);

        AccountingState.LedgerEntries.Add(new LedgerEntry
        {
            Id = Guid.NewGuid(),
            AccountNumber = account.AccountNumber,
            Description = "Account created with initial balance",
            Amount = account.Balance,
            CreatedAtUtc = DateTime.UtcNow
        });

        return Created($"/api/accounts/{account.AccountNumber}", account);
    }
}
