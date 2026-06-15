namespace CopyEngineService.Worker.Sagas;
using Microsoft.Extensions.Logging;

public sealed class CopyTradeSaga
{
    private readonly ILogger<CopyTradeSaga> _logger;

    public CopyTradeSaga(
        ILogger<CopyTradeSaga> logger)
    {
        _logger = logger;
    }

    public Task ProcessSignals()
    {
        _logger.LogInformation(
            "Processing trade copy saga");

        return Task.CompletedTask;
    }
}