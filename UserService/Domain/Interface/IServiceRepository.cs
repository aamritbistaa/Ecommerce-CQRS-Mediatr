using System;

namespace Domain.Interface;

public interface IServiceRepository<T> where T : class
{
    Task<List<T>> ListAll();
    Task<T> GetById(Guid id);
    Task<T?> AddItemAsync(T item);
    Task<bool> UpdateItemAsync(T item);
    Task<bool> RemoveAsync(T item);
}
