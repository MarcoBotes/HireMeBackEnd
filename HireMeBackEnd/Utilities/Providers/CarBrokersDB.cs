using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HireMeBackEnd.Models.Products;
using HireMeBackEnd.Utilities.Client;
using HireMeBackEnd.Models.Transactional;
using HireMeBackEnd.Models.Client;

namespace HireMeBackEnd.Utilities.Providers
{
    public class CarBrokersDB :
        ClientBase,
        ISource<Car>,
        ISource<Bike>,
        IDestination<Car>,
        IDestination<Bike>
    {
        private ILogger<CarBrokersDB>? _logger;
        private mockContext mockContext = new();
        public override void Configure(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<CarBrokersDB>>();
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

        void IDestination<Car>.DestinationHandler(object sender, List<ProductDelta<Car>> product)
        {
            throw new NotImplementedException();
        }

        void IDestination<Bike>.DestinationHandler(object sender, List<ProductDelta<Bike>> product)
        {
            throw new NotImplementedException();
        }

        List<ProductDelta<Bike>> ISource<Bike>.SourceProductDeltas()
        {
            return mockContext.Bikes.Select(b => new ProductDelta<Bike>
            {
                Operation = DeltaOperation.Add,
                Product = b
            }).ToList();
        }

        List<ProductDelta<Car>> ISource<Car>.SourceProductDeltas()
        {
            return mockContext.Cars.Select(c => new ProductDelta<Car>
            {
                Operation = DeltaOperation.Add,
                Product = c
            }).ToList();
        }
    }

    public class mockContext
    {
        public List<Car> Cars { get; set; } = new();
        public List<Bike> Bikes { get; set; } = new();
    }
}
