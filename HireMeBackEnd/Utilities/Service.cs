using HireMeBackEnd.Utilities.Client;

namespace HireMeBackEnd.Utilities
{
    public class Service
    {
        public List<ClientBase> Clients { get; set; }
        public async Task Run()
        {
            foreach (var client in Clients)
            {
                client.ProvideAllProducts();
            }
        }
    }
}
