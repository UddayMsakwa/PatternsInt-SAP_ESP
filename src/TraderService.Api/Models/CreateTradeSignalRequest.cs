namespace TraderService.Api.Models;

public sealed class CreateTradeSignalRequest
{
    public Guid TraderId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Side { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime TimestampUtc { get; set; }
}