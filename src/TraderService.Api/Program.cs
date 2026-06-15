using Microsoft.EntityFrameworkCore;
using TraderService.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<TraderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TraderDb")));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () => Results.Ok(new
{
    Service = "TraderService.Api",
    Status = "Running",
    TimestampUtc = DateTime.UtcNow
}));

app.Run();