using CopyEngineService.Worker.Models;
using Microsoft.Extensions.Logging;

namespace CopyEngineService.Worker.Services;

public sealed class ExchangeProxyClient(ILogger<ExchangeProxyClient> logger)
{
    public Task<bool> ExecuteCopiedOrder(TradeSignal signal, FollowerSubscription subscription)
    {
        var copiedQuantity = signal.Quantity * subscription.CopyRatio;

        logger.LogInformation(
            "Exchange Proxy simulated execution: account={AccountNumber}, symbol={Symbol}, side={Side}, quantity={Quantity}, price={Price}",
            subscription.FollowerAccountNumber,
            signal.Symbol,
            signal.Side,
            copiedQuantity,
            signal.Price);

        return Task.FromResult(true);
    }
}
