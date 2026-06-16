namespace Shared.Contracts.CQRS;

public sealed record GetSubscriptionsQuery(
    Guid TraderId);