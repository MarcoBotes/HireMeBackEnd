using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using HireMeBackEnd.Models.Products;
using HireMeBackEnd.Utilities.Client;
using HireMeBackEnd.Models.Transactional;
using HireMeBackEnd.Models.Client;

namespace HireMeBackEnd.Utilities.Providers
{
    public class CarGuysAPI :
        ClientBase,
        ISource<Car>,
        ISource<Bike>,
        IDestination<Car>,
        IDestination<Bike>
    {
        private ILogger<CarGuysAPI>? _logger;
        public override void Configure(IServiceProvider serviceProvider)
        {
            // Not strictly needed, but nice to have in this implementation.
            _logger = serviceProvider.GetRequiredService<ILogger<CarGuysAPI>>();
        }

        event EventHandler<List<ProductDelta<Car>>>? ISource<Car>.OnProductDelta
        {
            add
            {
                //throw new NotImplementedException();
            }

            remove
            {
                //throw new NotImplementedException();
            }
        }

        List<ProductDelta<Car>> ISource<Car>.SourceProductDeltas()
        {
            // In this example all of the deltas are assumed to be adds. But in reality you need to split them correctly and formulate the list explicitely.
            return GetEntitiesFromCarGuysAPI<Car>("url")
                .Select(e => new ProductDelta<Car>
                {
                    Operation = DeltaOperation.Add,
                    Product = e
                })
                .ToList();
        }

        void IDestination<Car>.DestinationHandler(object sender, List<ProductDelta<Car>> product)
        {
            throw new NotImplementedException();
        }

        event EventHandler<List<ProductDelta<Bike>>>? ISource<Bike>.OnProductDelta
        {
            add
            {
                //throw new NotImplementedException();
            }

            remove
            {
                //throw new NotImplementedException();
            }
        }

        List<ProductDelta<Bike>> ISource<Bike>.SourceProductDeltas()
        {
            // In this example all of the deltas are assumed to be adds. But in reality you need to split them correctly and formulate the list explicitely.
            return GetEntitiesFromCarGuysAPI<Bike>("url")
                .Select(e => new ProductDelta<Bike>
                {
                    Operation = DeltaOperation.Add,
                    Product = e
                })
                .ToList();
        }

        void IDestination<Bike>.DestinationHandler(object sender, List<ProductDelta<Bike>> product)
        {
            throw new NotImplementedException();
        }

        private string GetToken()
        {
            return "token";
        }

        private List<T> GetEntitiesFromCarGuysAPI<T>(string url) where T : IProduct
        {
            var token = GetToken();
            // This is where you would manage token, fire request, deserialize as type T, then return a list of the results
            return new List<T>();
        }
    }
}
