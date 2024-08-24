using Domain.Interface;
using Infrastructure.Repository;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceExtension
{
    public static void AddInfrastructure(this IServiceCollection service, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        service.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));

        service.AddScoped<IServiceFactory, ServiceFactory>();

        service.AddScoped<IUserService, UserService>();
        service.AddScoped<IUserCredentialsService, UserCredentialsService>();
        service.AddScoped<ICustomerService, CustomerService>();
    }

}
