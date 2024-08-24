using System;
using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Service;

public class UserService : IUserService
{
    private readonly IServiceFactory _factory;

    public UserService(IServiceFactory serviceFactory)
    {
        _factory = serviceFactory;
    }
    public async Task<List<EUser>> ListAllAsync()
    {
        var data = await _factory.GetInstance<EUser>().ListAll();
        return data;
    }
    public async Task<EUser> GetDataAsync(Guid Id)
    {
        var data = await _factory.GetInstance<EUser>().GetById(Id);
        return data;
    }
    public async Task<EUser> AddItemAsync(EUser item)
    {
        var data = await _factory.GetInstance<EUser>().AddItemAsync(item);
        return data;
    }
    public async Task<bool> UpdateItemAsync(EUser item)
    {
        var data = await _factory.GetInstance<EUser>().UpdateItemAsync(item);
        return data;
    }
    public async Task<bool> DeleteItemAsync(EUser item)
    {
        var data = await _factory.GetInstance<EUser>().RemoveAsync(item);
        return data;
    }
}
