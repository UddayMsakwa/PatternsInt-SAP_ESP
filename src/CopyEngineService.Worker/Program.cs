using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<CopyEngineService.Worker.Worker>();

var host = builder.Build();
host.Run();