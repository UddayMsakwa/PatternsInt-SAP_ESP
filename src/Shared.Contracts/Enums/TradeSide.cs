namespace Shared.Contracts.Enums;

public static class TradeSide
{
    public const string Buy = "BUY";
    public const string Sell = "SELL";

    public static bool IsValid(string value)
    {
        return value == Buy || value == Sell;
    }
}