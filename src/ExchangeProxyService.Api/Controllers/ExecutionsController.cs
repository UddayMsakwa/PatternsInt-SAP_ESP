using Microsoft.AspNetCore.Mvc;
using ExchangeProxyService.Api.Domain;
using ExchangeProxyService.Api.Models;

namespace ExchangeProxyService.Api.Controllers;

[ApiController]
[Route("api/executions")]
public sealed class ExecutionsController : ControllerBase
{
    private static readonly List<ExecutionResult> Results = [];

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Results);
    }

    [HttpPost]
    public IActionResult ExecuteTrade([FromBody] CreateExecutionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.AccountNumber))
        {
            return BadRequest(new { Message = "AccountNumber is required." });
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

        var executionRequest = new ExecutionRequest
        {
            Id = Guid.NewGuid(),
            SubscriptionId = request.SubscriptionId,
            AccountNumber = request.AccountNumber.Trim(),
            Symbol = request.Symbol.Trim().ToUpperInvariant(),
            Side = request.Side.Trim().ToUpperInvariant(),
            Quantity = request.Quantity,
            Price = request.Price,
            CreatedAtUtc = DateTime.UtcNow
        };

        var result = new ExecutionResult
        {
            Id = Guid.NewGuid(),
            ExecutionRequestId = executionRequest.Id,
            Success = true,
            ExecutionReference = $"EXEC-{Guid.NewGuid():N}",
            ExecutedAtUtc = DateTime.UtcNow
        };

        Results.Add(result);

        return Ok(result);
    }
}
