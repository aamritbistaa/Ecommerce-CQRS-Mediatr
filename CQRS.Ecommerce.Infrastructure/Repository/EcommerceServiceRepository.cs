using CQRS.Ecommerce.Domain;
using CQRS.Ecommerce.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Ecommerce.Infrastructure;

public class EcommerceServiceRepository<T> : IEcommerceServiceRepository<T> where T : class
{
    private readonly EcommerceDbContext db;
    private readonly DbSet<T> Entity;

    public EcommerceServiceRepository()
    {
        db = new EcommerceDbContext();
        Entity = db.Set<T>();
    }
    public EcommerceServiceRepository(EcommerceDbContext db)
    {
        this.db = db;
        Entity = db.Set<T>();
    }

    public async Task<List<T>> ListAllAsync()
    {
        var result = await Entity.ToListAsync();
        return result;
    }
    public async Task<T> GetByIdAsync(int id)
    {
        var result = await Entity.FindAsync(id);
        return result;
    }
    public async Task<T?> AddItemAsync(T item)
    {
        try
        {
            var result = await Entity.AddAsync(item);
            await db.SaveChangesAsync();
            return item;
        }
        catch (System.Exception)
        {
            return null;
        }
    }
    public async Task<bool> UpdateItemAsync(T item)
    {
        try
        {
            var result = Entity.Update(item);
            await db.SaveChangesAsync();
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public async Task<bool> RemoveAsync(T item)
    {
        try
        {
            var result = Entity.Remove(item);
            await db.SaveChangesAsync();
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }


}
