namespace AccountingService.Api.Domain;

public sealed class Position
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public decimal Quantity { get; set; }
}