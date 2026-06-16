using CopyEngineService.Worker.Models;
using Microsoft.Extensions.Logging;

namespace CopyEngineService.Worker.Services;

public sealed class AccountingClient(ILogger<AccountingClient> logger)
{
    public Task RecordCopiedExecution(TradeSignal signal, FollowerSubscription subscription)
    {
        var copiedQuantity = signal.Quantity * subscription.CopyRatio;

        logger.LogInformation(
            "Accounting simulated ledger update: account={AccountNumber}, symbol={Symbol}, side={Side}, quantity={Quantity}, price={Price}",
            subscription.FollowerAccountNumber,
            signal.Symbol,
            signal.Side,
            copiedQuantity,
            signal.Price);

        return Task.CompletedTask;
    }
}
