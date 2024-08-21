using System;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ServiceRepository<T> : IServiceRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<T> Entity;

    public ServiceRepository()
    {
        _db = new ApplicationDbContext();
        Entity = _db.Set<T>();
    }
    public ServiceRepository(ApplicationDbContext db)
    {
        _db = db;
        Entity = _db.Set<T>();
    }
    public async Task<List<T>> ListAll()
    {
        var result = await Entity.ToListAsync();
        return result;
    }
    public async Task<T> GetById(Guid id)
    {
        var result = await Entity.FindAsync(id);
        return result;
    }
    public async Task<T?> AddItemAsync(T item)
    {
        try
        {
            var result = await Entity.AddAsync(item);
            var res = await _db.SaveChangesAsync();
            return item;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while adding");
            return null;
        }
    }
    public async Task<bool> UpdateItemAsync(T item)
    {
        try
        {
            var result = Entity.Update(item);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while updating");
            return false;
        }
    }
    public async Task<bool> RemoveAsync(T item)
    {
        try
        {
            var result = Entity.Remove(item);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while deleting");
            return false;
        }
    }

}
