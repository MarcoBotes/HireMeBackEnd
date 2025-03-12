using HireMeBackEnd.Models.Products;

namespace HireMeBackEnd.Models.Transactional
{
    public class ProductDelta<TProduct> where TProduct : IProduct
    {
        public required TProduct Product { get; set; }
        public required DeltaOperation Operation { get; set; }
    }
}
