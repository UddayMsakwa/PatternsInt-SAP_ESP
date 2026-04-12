namespace Shared.Contracts.Common;

public sealed record MessageMetadata(
    Guid MessageId,
    DateTime TimestampUtc,
    string SourceService);