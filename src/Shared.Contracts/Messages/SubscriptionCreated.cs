namespace Shared.Contracts.Messages;

public sealed record SubscriptionCreated(
    Guid MessageId,
    Guid SubscriptionId,
    Guid FollowerUserId,
    string FollowerAccountNumber,
    Guid TraderId,
    string TraderAccountNumber,
    decimal CopyRatio,
    DateTime TimestampUtc);