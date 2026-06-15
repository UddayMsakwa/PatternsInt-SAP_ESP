namespace AccountingService.Api.Domain;

public sealed class LedgerEntry
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}