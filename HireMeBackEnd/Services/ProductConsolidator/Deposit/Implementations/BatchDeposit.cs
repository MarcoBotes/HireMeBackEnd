using HireMeBackEnd.Models.Products;

namespace HireMeBackEnd.Services.ProductConsolidator.Deposit.Implementations;

public class BatchDeposit : DepositHandler
{
    async public override void Deposit()
    {
        var channel = GetChannel<Car>();

        await foreach (var item in channel.Reader.ReadAllAsync())
        {
            Console.WriteLine($"Consumed: {item}");
        }
    }
}
