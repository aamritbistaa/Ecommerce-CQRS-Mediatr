using CQRS.Ecommerce.Domain.Entities;

namespace CQRS.Ecommerce.Domain;

public interface IProductService
{
    Task<List<Product>> GetAllProductAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task<bool> CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Product product);
}
