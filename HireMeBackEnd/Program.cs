using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NLog.Extensions.Logging;

using HireMeBackEnd.Models.Config;
using HireMeBackEnd.Services.ProductConsolidator;
using HireMeBackEnd.Services.ProductConsolidator.Collect;
using HireMeBackEnd.Services.ProductConsolidator.Collect.Implementations;
using HireMeBackEnd.Services.ProductConsolidator.Deposit;
using HireMeBackEnd.Services.ProductConsolidator.Deposit.Implementations;

namespace HireMeBackEnd
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddNLog();
                })
                .UseWindowsService()
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<ProductConsolidator>();

                    services.Configure<Config>(context.Configuration);

                    services.AddSingleton<CollectHandler>(serviceProvider =>
                    {
                        var configMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<Config>>();
                        var logger = serviceProvider.GetRequiredService<ILogger<QueryAll>>();

                        return configMonitor.CurrentValue.Collect.Strategy switch
                        {
                            CollectStrategy.QueryAll => new QueryAll(logger),
                            _ => throw new InvalidOperationException($"Unknown strategy, {configMonitor.CurrentValue.Collect.Strategy}")
                        };
                    });

                    services.AddSingleton<DepositHandler>(serviceProvider =>
                    {
                        var configMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<Config>>();

                        return configMonitor.CurrentValue.Deposit.Strategy switch
                        {
                            DepositStrategy.Batch => new BatchDeposit(),
                            _ => throw new InvalidOperationException($"Unknown strategy, {configMonitor.CurrentValue.Collect.Strategy}")
                        };
                    });
                });

            await builder.Build().RunAsync();
        }
    }
}
