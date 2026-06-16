namespace Shared.Contracts.CQRS;

public sealed record CreateSubscriptionCommand(
    Guid UserId,
    Guid TraderId);