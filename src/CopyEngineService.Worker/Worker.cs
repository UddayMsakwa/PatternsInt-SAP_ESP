namespace CopyEngineService.Worker;

public sealed class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("CopyEngineService.Worker started at {TimeUtc}", DateTime.UtcNow);

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("CopyEngineService.Worker heartbeat at {TimeUtc}", DateTime.UtcNow);
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }

        logger.LogInformation("CopyEngineService.Worker stopped at {TimeUtc}", DateTime.UtcNow);
    }
}