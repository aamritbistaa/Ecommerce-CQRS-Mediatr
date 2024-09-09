using System;
using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Service;

public class CountryService : ICountryService
{
    private readonly IServiceFactory _factory;

    public CountryService(IServiceFactory factory)
    {
        _factory = factory;
    }
    public async Task<List<ECountryAddress>> ListAllAsync()
    {
        var data = await _factory.GetInstance<ECountryAddress>().ListAll();
        data = data.Where(x => x.IsDeleted == false).ToList();
        return data;
    }
    public async Task<ECountryAddress> GetDataAsync(Guid Id)
    {
        var data = await _factory.GetInstance<ECountryAddress>().GetById(Id);
        return data;
    }
    public async Task<ECountryAddress> AddItemAsync(ECountryAddress item)
    {
        var data = await _factory.GetInstance<ECountryAddress>().AddItemAsync(item);
        return data;
    }
    public async Task<bool> UpdateItemAsync(ECountryAddress item)
    {
        var data = await _factory.GetInstance<ECountryAddress>().UpdateItemAsync(item);
        return data;
    }

}
