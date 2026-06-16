using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Api.Domain;
using SubscriptionService.Api.Models;

namespace SubscriptionService.Api.Controllers;

[ApiController]
[Route("api/subscriptions")]
public sealed class SubscriptionsController : ControllerBase
{
    private static readonly List<Subscription> Subscriptions =
    [
        new Subscription
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            FollowerUserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            FollowerAccountNumber = "FOLLOWER-001",
            TraderId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            TraderAccountNumber = "TRADER-001",
            CopyRatio = 1.0m,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        }
    ];

    [HttpGet]
    public ActionResult<IReadOnlyCollection<SubscriptionResponse>> GetAll()
    {
        return Ok(Subscriptions.Select(ToResponse).ToList());
    }

    [HttpGet("by-trader/{traderId:guid}")]
    public ActionResult<IReadOnlyCollection<SubscriptionResponse>> GetByTrader(Guid traderId)
    {
        var result = Subscriptions
            .Where(x => x.TraderId == traderId && x.IsActive)
            .Select(ToResponse)
            .ToList();

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<SubscriptionResponse> Create([FromBody] CreateSubscriptionRequest request)
    {
        if (request.UserId == Guid.Empty)
        {
            return BadRequest(new { Message = "UserId is required." });
        }

        if (request.TraderId == Guid.Empty)
        {
            return BadRequest(new { Message = "TraderId is required." });
        }

        if (string.IsNullOrWhiteSpace(request.UserAccountNumber))
        {
            return BadRequest(new { Message = "UserAccountNumber is required." });
        }

        if (string.IsNullOrWhiteSpace(request.TraderAccountNumber))
        {
            return BadRequest(new { Message = "TraderAccountNumber is required." });
        }

        var subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            FollowerUserId = request.UserId,
            FollowerAccountNumber = request.UserAccountNumber.Trim(),
            TraderId = request.TraderId,
            TraderAccountNumber = request.TraderAccountNumber.Trim(),
            CopyRatio = request.CopyRatio <= 0 ? 1.0m : request.CopyRatio,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };

        Subscriptions.Add(subscription);

        return Created($"/api/subscriptions/{subscription.Id}", ToResponse(subscription));
    }

    private static SubscriptionResponse ToResponse(Subscription subscription)
    {
        return new SubscriptionResponse
        {
            Id = subscription.Id,
            FollowerUserId = subscription.FollowerUserId,
            FollowerAccountNumber = subscription.FollowerAccountNumber,
            TraderId = subscription.TraderId,
            TraderAccountNumber = subscription.TraderAccountNumber,
            CopyRatio = subscription.CopyRatio,
            IsActive = subscription.IsActive,
            CreatedAtUtc = subscription.CreatedAtUtc
        };
    }
}
