using CopyEngineService.Worker.Sagas;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace CopyEngineService.Worker;

public sealed class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly CopyTradeSaga _saga;

    public Worker(
        ILogger<Worker> logger,
        CopyTradeSaga saga)
    {
        _logger = logger;
        _saga = saga;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _saga.ProcessSignals();

            await Task.Delay(
                TimeSpan.FromSeconds(15),
                stoppingToken);
        }
    }
}