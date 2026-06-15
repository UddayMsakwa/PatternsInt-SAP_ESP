namespace Shared.Contracts.EventSourcing;

public sealed class EventStoreRecord
{
    public Guid Id { get; set; }

    public string AggregateType { get; set; } = string.Empty;

    public Guid AggregateId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string EventData { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
}