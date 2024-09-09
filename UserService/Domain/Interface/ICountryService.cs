using System;
using Domain.Entity;

namespace Domain.Interface;

public interface ICountryService
{
    Task<List<ECountryAddress>> ListAllAsync();
    Task<ECountryAddress> GetDataAsync(Guid Id);
    Task<ECountryAddress> AddItemAsync(ECountryAddress item);
    Task<bool> UpdateItemAsync(ECountryAddress item);
}
