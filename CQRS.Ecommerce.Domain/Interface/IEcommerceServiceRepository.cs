namespace CQRS.Ecommerce.Domain;

public interface IEcommerceServiceRepository<T> where T : class
{
    Task<List<T>> ListAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddItemAsync(T item);
    Task<bool> UpdateItemAsync(T item);
    Task<bool> RemoveAsync(T item);
}
