using HireMeBackEnd.Models.Products;
using HireMeBackEnd.Models.Transactional;

namespace HireMeBackEnd.Models.Client
{
    public interface IDestination<TProduct> where TProduct : IProduct
    {
        public void DestinationHandler(object sender, List<ProductDelta<TProduct>> product);
    }
}
