namespace Shared.Contracts.CQRS;

public sealed record CreateTraderCommand(
    string Name,
    string AccountNumber);