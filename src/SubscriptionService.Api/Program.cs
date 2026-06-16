using Microsoft.EntityFrameworkCore;
using SubscriptionService.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<SubscriptionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SubscriptionDb")));

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => Results.Ok(new
{
    Service = "SubscriptionService.Api",
    Status = "Running",
    TimestampUtc = DateTime.UtcNow
}));

app.Run();