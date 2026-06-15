namespace TraderService.Api.Models;

public sealed class TradeSignalResponse
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
}