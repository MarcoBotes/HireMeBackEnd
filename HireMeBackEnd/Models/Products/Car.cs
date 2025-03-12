namespace HireMeBackEnd.Models.Products;

public class Car : IProduct
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
