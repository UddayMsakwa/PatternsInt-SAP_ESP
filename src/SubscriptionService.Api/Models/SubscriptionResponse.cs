namespace SubscriptionService.Api.Models;

public sealed class SubscriptionResponse
{
    public Guid Id { get; set; }
    public Guid FollowerUserId { get; set; }
    public string FollowerAccountNumber { get; set; } = string.Empty;
    public Guid TraderId { get; set; }
    public string TraderAccountNumber { get; set; } = string.Empty;
    public decimal CopyRatio { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}