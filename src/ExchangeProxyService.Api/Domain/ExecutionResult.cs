namespace ExchangeProxyService.Api.Domain;

public sealed class ExecutionResult
{
    public Guid Id { get; set; }

    public Guid ExecutionRequestId { get; set; }

    public bool Success { get; set; }

    public string ExecutionReference { get; set; } = string.Empty;

    public DateTime ExecutedAtUtc { get; set; }
}