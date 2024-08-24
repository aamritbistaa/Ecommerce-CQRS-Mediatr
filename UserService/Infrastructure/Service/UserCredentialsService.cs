using Domain.Entity;
using Domain.Interface;

namespace Infrastructure.Service;

public class UserCredentialsService : IUserCredentialsService
{
    private readonly IServiceFactory _factory;

    public UserCredentialsService(IServiceFactory serviceFactory)
    {
        _factory = serviceFactory;
    }
    public async Task<List<EUserCredentials>> ListAllAsync()
    {
        var data = await _factory.GetInstance<EUserCredentials>().ListAll();
        return data;
    }
    public async Task<EUserCredentials?> GetByEmail(string email)
    {
        var data = await _factory.GetInstance<EUserCredentials>().ListAll();

        return data.FirstOrDefault(x => x.Email == email);

    }
    public async Task<EUserCredentials> GetDataAsync(Guid Id)
    {
        var data = await _factory.GetInstance<EUserCredentials>().GetById(Id);
        return data;
    }
    public async Task<EUserCredentials> AddItemAsync(EUserCredentials item)
    {
        var data = await _factory.GetInstance<EUserCredentials>().AddItemAsync(item);
        return data;
    }
    public async Task<bool> UpdateItemAsync(EUserCredentials item)
    {
        var data = await _factory.GetInstance<EUserCredentials>().UpdateItemAsync(item);
        return data;
    }
    public async Task<bool> DeleteItemAsync(EUserCredentials item)
    {
        var data = await _factory.GetInstance<EUserCredentials>().RemoveAsync(item);
        return data;
    }
}
