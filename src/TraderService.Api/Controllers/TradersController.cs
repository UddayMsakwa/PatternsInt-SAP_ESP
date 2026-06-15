using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraderService.Api.Domain;
using TraderService.Api.Models;
using TraderService.Api.Persistence;

namespace TraderService.Api.Controllers;

[ApiController]
[Route("api/traders")]
public sealed class TradersController(TraderDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TraderResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var traders = await dbContext.Traders
            .OrderBy(x => x.CreatedAtUtc)
            .Select(x => new TraderResponse
            {
                Id = x.Id,
                Name = x.Name,
                AccountNumber = x.AccountNumber,
                IsActive = x.IsActive,
                CreatedAtUtc = x.CreatedAtUtc
            })
            .ToListAsync(cancellationToken);

        return Ok(traders);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TraderResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var trader = await dbContext.Traders
            .Where(x => x.Id == id)
            .Select(x => new TraderResponse
            {
                Id = x.Id,
                Name = x.Name,
                AccountNumber = x.AccountNumber,
                IsActive = x.IsActive,
                CreatedAtUtc = x.CreatedAtUtc
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (trader is null)
        {
            return NotFound(new { Message = "Trader not found." });
        }

        return Ok(trader);
    }

    [HttpPost]
    public async Task<ActionResult<TraderResponse>> Create(
        [FromBody] CreateTraderRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { Message = "Name is required." });
        }

        if (string.IsNullOrWhiteSpace(request.AccountNumber))
        {
            return BadRequest(new { Message = "AccountNumber is required." });
        }

        var existing = await dbContext.Traders
            .AnyAsync(x => x.AccountNumber == request.AccountNumber, cancellationToken);

        if (existing)
        {
            return Conflict(new { Message = "A trader with this account number already exists." });
        }

        var trader = new Trader
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            AccountNumber = request.AccountNumber.Trim(),
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };

        dbContext.Traders.Add(trader);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new TraderResponse
        {
            Id = trader.Id,
            Name = trader.Name,
            AccountNumber = trader.AccountNumber,
            IsActive = trader.IsActive,
            CreatedAtUtc = trader.CreatedAtUtc
        };

        return CreatedAtAction(nameof(GetById), new { id = trader.Id }, response);
    }
}