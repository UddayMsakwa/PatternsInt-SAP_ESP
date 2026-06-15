using Microsoft.Extensions.Hosting;
using CopyEngineService.Worker;
using CopyEngineService.Worker.Sagas;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddSingleton<CopyTradeSaga>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();