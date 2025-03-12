using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NLog.Extensions.Logging;
using HireMeBackEnd.Services;
using HireMeBackEnd.Models;

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
                });

            await builder.Build().RunAsync();
        }
    }
}
