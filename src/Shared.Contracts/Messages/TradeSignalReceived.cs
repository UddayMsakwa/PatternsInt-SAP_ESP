namespace Shared.Contracts.Messages;

public sealed record TradeSignalReceived(
    Guid MessageId,
    Guid TraderId,
    string TraderAccountNumber,
    string Symbol,
    string Side,
    decimal Quantity,
    decimal Price,
    DateTime TimestampUtc);