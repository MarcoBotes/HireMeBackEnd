using HireMeBackEnd.Models.Config;
using HireMeBackEnd.Models.Products;
using HireMeBackEnd.Services.ProductConsolidator.Collect;
using HireMeBackEnd.Services.ProductConsolidator.Deposit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace HireMeBackEnd.Services.ProductConsolidator
{
    internal class ProductConsolidator : BackgroundService
    {
        private readonly ILogger<ProductConsolidator> logger;
        private readonly CollectHandler collectHandler;
        private readonly DepositHandler depositHandler;

        public ProductConsolidator(ILogger<ProductConsolidator> logger, CollectHandler collectionStrategy, DepositHandler depositStrategy)
        {
            this.logger = logger;
            this.collectHandler = collectionStrategy;
            this.depositHandler = depositStrategy;

            CreateChannel<Car>();
            CreateChannel<Bike>();
        }

        private void CreateChannel<TEntity>()
        {
            var channel = Channel.CreateBounded<TEntity>(5);
            collectHandler.RegisterChannel(channel);
            depositHandler.RegisterChannel(channel);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                collectHandler.Collect();
                depositHandler.Deposit();
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
