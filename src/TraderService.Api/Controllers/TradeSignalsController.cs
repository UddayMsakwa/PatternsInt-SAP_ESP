using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Enums;
using Shared.Contracts.Messages;
using TraderService.Api.Domain;
using TraderService.Api.Models;
using TraderService.Api.Persistence;

namespace TraderService.Api.Controllers;

[ApiController]
[Route("api/trades")]
public sealed class TradeSignalsController(TraderDbContext dbContext) : ControllerBase
{
    [HttpPost("signals")]
    public async Task<ActionResult<TradeSignalResponse>> CreateSignal(
        [FromBody] CreateTradeSignalRequest request,
        CancellationToken cancellationToken)
    {
        if (!TradeSide.IsValid(request.Side))
        {
            return BadRequest(new { Message = "Side must be BUY or SELL." });
        }

        if (string.IsNullOrWhiteSpace(request.Symbol))
        {
            return BadRequest(new { Message = "Symbol is required." });
        }

        if (request.Quantity <= 0)
        {
            return BadRequest(new { Message = "Quantity must be greater than zero." });
        }

        if (request.Price <= 0)
        {
            return BadRequest(new { Message = "Price must be greater than zero." });
        }

        var trader = await dbContext.Traders
            .FirstOrDefaultAsync(x => x.Id == request.TraderId, cancellationToken);

        if (trader is null)
        {
            return NotFound(new { Message = "Trader not found." });
        }

        var signal = new TradeSignal
        {
            Id = Guid.NewGuid(),
            TraderId = trader.Id,
            TraderAccountNumber = trader.AccountNumber,
            Symbol = request.Symbol.Trim().ToUpperInvariant(),
            Side = request.Side.Trim().ToUpperInvariant(),
            Quantity = request.Quantity,
            Price = request.Price,
            SignalTimestampUtc = request.TimestampUtc,
            CreatedAtUtc = DateTime.UtcNow
        };

        var integrationEvent = new TradeSignalReceived(
            MessageId: Guid.NewGuid(),
            TraderId: signal.TraderId,
            TraderAccountNumber: signal.TraderAccountNumber,
            Symbol: signal.Symbol,
            Side: signal.Side,
            Quantity: signal.Quantity,
            Price: signal.Price,
            TimestampUtc: signal.SignalTimestampUtc
        );

        var outbox = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            MessageType = nameof(TradeSignalReceived),
            Payload = JsonSerializer.Serialize(integrationEvent),
            OccurredAtUtc = DateTime.UtcNow
        };

        dbContext.TradeSignals.Add(signal);
        dbContext.OutboxMessages.Add(outbox);

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new TradeSignalResponse
        {
            Id = signal.Id,
            TraderId = signal.TraderId,
            TraderAccountNumber = signal.TraderAccountNumber,
            Symbol = signal.Symbol,
            Side = signal.Side,
            Quantity = signal.Quantity,
            Price = signal.Price,
            SignalTimestampUtc = signal.SignalTimestampUtc,
            CreatedAtUtc = signal.CreatedAtUtc
        };

        return Ok(response);
    }

    [HttpGet("signals")]
    public async Task<ActionResult<IReadOnlyCollection<TradeSignalResponse>>> GetSignals(CancellationToken cancellationToken)
    {
        var signals = await dbContext.TradeSignals
            .OrderByDescending(x => x.SignalTimestampUtc)
            .Select(x => new TradeSignalResponse
            {
                Id = x.Id,
                TraderId = x.TraderId,
                TraderAccountNumber = x.TraderAccountNumber,
                Symbol = x.Symbol,
                Side = x.Side,
                Quantity = x.Quantity,
                Price = x.Price,
                SignalTimestampUtc = x.SignalTimestampUtc,
                CreatedAtUtc = x.CreatedAtUtc
            })
            .ToListAsync(cancellationToken);

        return Ok(signals);
    }
}