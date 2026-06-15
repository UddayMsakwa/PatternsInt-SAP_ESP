namespace TraderService.Api.Models;

public sealed class CreateTraderRequest
{
    public string Name { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
}