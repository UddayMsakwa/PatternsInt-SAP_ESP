namespace Shared.Contracts.CQRS;

public sealed record GetTraderQuery(
    Guid TraderId);