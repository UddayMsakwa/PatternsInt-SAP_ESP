namespace CopyEngineService.Worker.Models;

public sealed class TradeSignal
{
    public Guid TraderId { get; set; }
    public string TraderAccountNumber { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string Side { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
}
