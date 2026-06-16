namespace CopyEngineService.Worker.Models;

public sealed class CopyTradeResult
{
    public bool Success { get; set; }
    public int FollowersProcessed { get; set; }
    public string Message { get; set; } = string.Empty;
}
