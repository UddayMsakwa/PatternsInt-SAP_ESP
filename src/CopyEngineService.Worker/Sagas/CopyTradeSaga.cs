using CopyEngineService.Worker.Models;
using CopyEngineService.Worker.Services;
using Microsoft.Extensions.Logging;

namespace CopyEngineService.Worker.Sagas;

public sealed class CopyTradeSaga(
    ILogger<CopyTradeSaga> logger,
    SubscriptionLookupService subscriptionLookupService,
    ExchangeProxyClient exchangeProxyClient,
    AccountingClient accountingClient)
{
    public async Task<CopyTradeResult> ProcessSignals()
    {
        logger.LogInformation("Processing trade copy saga");

        var signal = new TradeSignal
        {
            TraderId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            TraderAccountNumber = "TRADER-001",
            Symbol = "AAPL",
            Side = "BUY",
            Quantity = 10m,
            Price = 185.50m
        };

        var subscriptions = subscriptionLookupService.GetActiveSubscriptions(signal.TraderId);
        var processed = 0;

        foreach (var subscription in subscriptions)
        {
            var exchangeSuccess = await exchangeProxyClient.ExecuteCopiedOrder(signal, subscription);

            if (!exchangeSuccess)
            {
                logger.LogWarning(
                    "Exchange execution failed for follower account {AccountNumber}. Compensation would release reservation.",
                    subscription.FollowerAccountNumber);

                continue;
            }

            await accountingClient.RecordCopiedExecution(signal, subscription);
            processed++;
        }

        var result = new CopyTradeResult
        {
            Success = true,
            FollowersProcessed = processed,
            Message = $"Copy trade completed for {processed} follower account(s)."
        };

        logger.LogInformation(result.Message);

        return result;
    }
}
