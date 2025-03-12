using HireMeBackEnd.Models.Client;
using HireMeBackEnd.Models.Products;
using HireMeBackEnd.Utilities.Client;

namespace HireMeBackEnd.Utilities.Builders
{
    public class ClientBuilder<TClient> where TClient : ClientBase, new()
    {
        private List<Type> provides = new List<Type>();
        public ClientBuilder<TClient> Provide<TProduct>() where TProduct : IProduct
        {
            if (!typeof(ISource<TProduct>).IsAssignableFrom(typeof(TClient))) throw new InvalidOperationException($"{typeof(TClient).Name} must implement ISource<{typeof(TProduct).Name}>");
            provides.Add(typeof(TProduct));
            return this;
        }

        private List<Type> consumes = new List<Type>();
        public ClientBuilder<TClient> Consume<TProduct>() where TProduct : IProduct
        {
            if (!typeof(IDestination<TProduct>).IsAssignableFrom(typeof(TClient))) throw new InvalidOperationException($"{typeof(TClient).Name} must implement IDestination<{typeof(TProduct).Name}>");
            consumes.Add(typeof(TProduct));
            return this;
        }

        public TClient Build()
        {
            var newClient = new TClient();
            newClient.Consumes(consumes);
            newClient.Provides(provides);
            return newClient;
        }
    }
}
