using Microsoft.AspNetCore.Mvc;
using AccountingService.Api.Domain;

namespace AccountingService.Api.Controllers;

[ApiController]
[Route("api/accounts")]
public sealed class AccountsController : ControllerBase
{
    private static readonly List<Account> Accounts = [];

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Accounts);
    }

    [HttpPost]
    public IActionResult Create(Account account)
    {
        account.Id = Guid.NewGuid();

        Accounts.Add(account);

        return Ok(account);
    }
}