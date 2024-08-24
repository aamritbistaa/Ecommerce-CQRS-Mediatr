using System;
using Domain.Entity;

namespace Domain.Interface;

public interface IUserCredentialsService
{
    Task<List<EUserCredentials>> ListAllAsync();
    Task<EUserCredentials?> GetByEmail(string email);
    Task<EUserCredentials> GetDataAsync(Guid Id);
    Task<EUserCredentials> AddItemAsync(EUserCredentials item);
    Task<bool> UpdateItemAsync(EUserCredentials item);
    Task<bool> DeleteItemAsync(EUserCredentials item);
}
