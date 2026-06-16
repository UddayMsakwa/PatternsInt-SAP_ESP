namespace CopyEngineService.Worker.Models;

public sealed class FollowerSubscription
{
    public Guid SubscriptionId { get; set; }
    public Guid FollowerUserId { get; set; }
    public string FollowerAccountNumber { get; set; } = string.Empty;
    public decimal CopyRatio { get; set; }
}
