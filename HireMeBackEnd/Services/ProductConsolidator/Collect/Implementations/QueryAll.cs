using HireMeBackEnd.Models.Products;
using HireMeBackEnd.Services.ProductConsolidator.Deposit;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace HireMeBackEnd.Services.ProductConsolidator.Collect.Implementations;

public class QueryAll : CollectHandler
{
    private readonly ILogger<QueryAll> logger;
    public QueryAll(ILogger<QueryAll> logger)
    {
        this.logger = logger;
    }

    public async override void Collect()
    {
        var channel = GetChannel<Car>();

        for (int i = 0; i < 5; i++)
        {
            await channel.Writer.WriteAsync(new Car
            {
                Id = i,
                Name = i.ToString(),
            });
        }

        channel.Writer.Complete();
    }
}
