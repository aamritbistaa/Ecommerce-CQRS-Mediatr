using System;
using Domain.Entity;

namespace Domain.Interface;

public interface ICustomerService
{
    Task<List<ECustomer>> ListAllAsync();
    Task<ECustomer> GetDataAsync(Guid Id);
    Task<ECustomer> AddItemAsync(ECustomer item);
    Task<bool> UpdateItemAsync(ECustomer item);
    Task<bool> DeleteItemAsync(ECustomer item);
}
