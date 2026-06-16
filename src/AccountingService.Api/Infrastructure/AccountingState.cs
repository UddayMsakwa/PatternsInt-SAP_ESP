using AccountingService.Api.Domain;

namespace AccountingService.Api.Infrastructure;

public static class AccountingState
{
    public static readonly List<Account> Accounts =
    [
        new Account
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            AccountNumber = "FOLLOWER-001",
            Balance = 100000m
        },
        new Account
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            AccountNumber = "TRADER-001",
            Balance = 250000m
        }
    ];

    public static readonly List<Position> Positions =
    [
        new Position
        {
            Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            AccountNumber = "FOLLOWER-001",
            Symbol = "AAPL",
            Quantity = 10m
        }
    ];

    public static readonly List<LedgerEntry> LedgerEntries =
    [
        new LedgerEntry
        {
            Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            AccountNumber = "FOLLOWER-001",
            Description = "Initial simulated deposit",
            Amount = 100000m,
            CreatedAtUtc = DateTime.UtcNow
        }
    ];
}
