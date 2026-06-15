namespace SubscriptionService.Api.Models;

public sealed class CreateSubscriptionRequest
{
    public Guid UserId { get; set; }
    public string UserAccountNumber { get; set; } = string.Empty;
    public Guid TraderId { get; set; }
    public string TraderAccountNumber { get; set; } = string.Empty;
    public decimal CopyRatio { get; set; } = 1.0m;
    public DateTime TimestampUtc { get; set; }
}