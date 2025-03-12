namespace HireMeBackEnd.Models.Products
{
    public class Bike : IProduct
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
