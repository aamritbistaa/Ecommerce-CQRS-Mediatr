using System;
using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Service;

public class CustomerService : ICustomerService
{
    private readonly IServiceFactory _factory;

    public CustomerService(IServiceFactory serviceFactory)
    {
        _factory = serviceFactory;
    }
    public async Task<List<ECustomer>> ListAllAsync()
    {
        var data = await _factory.GetInstance<ECustomer>().ListAll();
        return data;
    }
    public async Task<ECustomer> GetDataAsync(Guid Id)
    {
        var data = await _factory.GetInstance<ECustomer>().GetById(Id);
        return data;
    }
    public async Task<ECustomer> AddItemAsync(ECustomer item)
    {
        var data = await _factory.GetInstance<ECustomer>().AddItemAsync(item);
        return data;
    }
    public async Task<bool> UpdateItemAsync(ECustomer item)
    {
        var data = await _factory.GetInstance<ECustomer>().UpdateItemAsync(item);
        return data;
    }
    public async Task<bool> DeleteItemAsync(ECustomer item)
    {
        var data = await _factory.GetInstance<ECustomer>().RemoveAsync(item);
        return data;
    }
}
