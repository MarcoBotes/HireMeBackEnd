using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using HireMeBackEnd.Models.Products;
using HireMeBackEnd.Utilities.Builders;
using HireMeBackEnd.Utilities;
using HireMeBackEnd.Utilities.Providers;

namespace HireMeBackEnd.Services
{
    internal class ProductConsolidator : BackgroundService
    {
        private readonly ILogger<ProductConsolidator> logger;

        private readonly Service Service;

        public ProductConsolidator(ILogger<ProductConsolidator> _logger)
        {
            logger = _logger;

            Service = ServiceBuilder.DefaultServiceBuilder()
                .AddClient<CarGuysAPI>(builder =>
                {
                    builder
                        .Consume<Car>()
                        .Consume<Bike>()

                        .Provide<Car>()
                        .Provide<Bike>();
                })
                .AddClient<CarBrokersDB>(builder =>
                {
                    builder
                        .Consume<Car>()
                        .Consume<Bike>()

                        .Provide<Car>()
                        .Provide<Bike>();
                })
                .Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Service.Run();
        }
    }
}
