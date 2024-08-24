using System;
using Domain.Entity;

namespace Domain.Interface;

public interface IUserService
{
    Task<List<EUser>> ListAllAsync();
    Task<EUser> GetDataAsync(Guid Id);
    Task<EUser> AddItemAsync(EUser item);
    Task<bool> UpdateItemAsync(EUser item);
    Task<bool> DeleteItemAsync(EUser item);
}
