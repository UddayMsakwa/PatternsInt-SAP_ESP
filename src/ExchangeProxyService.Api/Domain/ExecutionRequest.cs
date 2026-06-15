namespace ExchangeProxyService.Api.Domain;

public sealed class ExecutionRequest
{
    public Guid Id { get; set; }

    public Guid SubscriptionId { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public string Side { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}