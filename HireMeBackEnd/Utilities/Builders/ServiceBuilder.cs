using HireMeBackEnd.Models.Client;
using HireMeBackEnd.Models.Products;
using HireMeBackEnd.Utilities.Client;

namespace HireMeBackEnd.Utilities.Builders
{
    public class ServiceBuilder
    {
        public static ServiceBuilder DefaultServiceBuilder() => new ServiceBuilder();

        private Service Service { get; set; } = new Service();
        public Service Build()
        {
            foreach (var sourceClient in Clients)
            {
                var sourceType = sourceClient.GetType();

                foreach (var interfaceType in sourceType.GetInterfaces())
                {
                    if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ISource<>))
                    {
                        Type productType = interfaceType.GetGenericArguments()[0];

                        var eventInfo = interfaceType.GetEvent("OnProductDelta");
                        if (eventInfo == null) continue;

                        foreach (var destinationClient in Clients)
                        {
                            if (destinationClient == sourceClient) continue;

                            var destinationType = destinationClient.GetType();
                            var destinationInterface = typeof(IDestination<>).MakeGenericType(productType);

                            if (destinationInterface.IsAssignableFrom(destinationType))
                            {
                                // Find the correct DestinationHandler overload
                                var handlerMethod = destinationInterface
                                    .GetMethod("DestinationHandler");

                                if (handlerMethod == null) continue;

                                // Create the delegate and subscribe
                                var handlerDelegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, destinationClient, handlerMethod);
                                eventInfo.AddEventHandler(sourceClient, handlerDelegate);

                                Console.WriteLine($"{destinationClient} subscribed to {sourceClient}'s {productType.Name} updates.");
                            }
                        }
                    }
                }
            }

            Service.Clients = Clients;
            return Service;
        }

        private List<ClientBase> Clients { get; set; } = new();
        public ServiceBuilder AddClient<TClient>(Action<ClientBuilder<TClient>> configure) where TClient : ClientBase, new()
        {
            var clientBuilder = new ClientBuilder<TClient>();
            configure(clientBuilder);
            Clients.Add(clientBuilder.Build());
            return this;
        }
    }
}
