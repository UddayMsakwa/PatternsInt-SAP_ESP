using CopyEngineService.Worker;
using CopyEngineService.Worker.Sagas;
using CopyEngineService.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<SubscriptionLookupService>();
builder.Services.AddSingleton<ExchangeProxyClient>();
builder.Services.AddSingleton<AccountingClient>();
builder.Services.AddSingleton<CopyTradeSaga>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
