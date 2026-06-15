using Microsoft.AspNetCore.Mvc;
using ExchangeProxyService.Api.Domain;
using ExchangeProxyService.Api.Models;

namespace ExchangeProxyService.Api.Controllers;

[ApiController]
[Route("api/executions")]
public sealed class ExecutionsController : ControllerBase
{
    [HttpPost]
    public IActionResult ExecuteTrade(
        [FromBody] CreateExecutionRequest request)
    {
        var executionRequest = new ExecutionRequest
        {
            Id = Guid.NewGuid(),
            SubscriptionId = request.SubscriptionId,
            AccountNumber = request.AccountNumber,
            Symbol = request.Symbol,
            Side = request.Side,
            Quantity = request.Quantity,
            Price = request.Price,
            CreatedAtUtc = DateTime.UtcNow
        };

        var result = new ExecutionResult
        {
            Id = Guid.NewGuid(),
            ExecutionRequestId = executionRequest.Id,
            Success = true,
            ExecutionReference = $"EXEC-{Guid.NewGuid()}",
            ExecutedAtUtc = DateTime.UtcNow
        };

        return Ok(result);
    }
}