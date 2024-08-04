using CQRS.Ecommerce.Domain.Entities;

namespace CQRS.Ecommerce.Domain;

public interface IProductService
{
    Task<List<Product>> GetAllProduct();
    Task<Product> GetProductById(int id);
    Task<bool> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Product product);
}
