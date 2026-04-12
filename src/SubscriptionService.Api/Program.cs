var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () => Results.Ok(new
{
    Service = "SubscriptionService.Api",
    Status = "Running",
    TimestampUtc = DateTime.UtcNow
}));

app.Run();