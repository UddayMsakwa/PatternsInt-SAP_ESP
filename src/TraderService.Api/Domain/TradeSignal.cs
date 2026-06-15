namespace TraderService.Api.Domain;

public sealed class TradeSignal
{
    public Guid Id { get; set; }
    public Guid TraderId { get; set; }
    public string TraderAccountNumber { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string Side { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime SignalTimestampUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public Trader? Trader { get; set; }
}