using HireMeBackEnd.Models.Products;
using HireMeBackEnd.Models.Transactional;

namespace HireMeBackEnd.Models.Client
{
    public interface ISource<TProduct> where TProduct : IProduct
    {
        event EventHandler<List<ProductDelta<TProduct>>>? OnProductDelta;

        public List<ProductDelta<TProduct>> SourceProductDeltas();
    }
}
