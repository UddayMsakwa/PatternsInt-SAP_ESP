namespace ExchangeProxyService.Api.Models;

public sealed class CreateExecutionRequest
{
    public Guid SubscriptionId { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public string Side { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }
}