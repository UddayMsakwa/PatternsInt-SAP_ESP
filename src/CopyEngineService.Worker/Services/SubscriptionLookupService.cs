using CopyEngineService.Worker.Models;

namespace CopyEngineService.Worker.Services;

public sealed class SubscriptionLookupService
{
    public IReadOnlyCollection<FollowerSubscription> GetActiveSubscriptions(Guid traderId)
    {
        return
        [
            new FollowerSubscription
            {
                SubscriptionId = Guid.NewGuid(),
                FollowerUserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                FollowerAccountNumber = "FOLLOWER-001",
                CopyRatio = 1.0m
            },
            new FollowerSubscription
            {
                SubscriptionId = Guid.NewGuid(),
                FollowerUserId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                FollowerAccountNumber = "FOLLOWER-002",
                CopyRatio = 0.5m
            }
        ];
    }
}
