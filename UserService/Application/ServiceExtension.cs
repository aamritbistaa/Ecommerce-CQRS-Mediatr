using System;
using Application.Helper;
using Application.Manager.Implementation;
using Application.Manager.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceExtension
{
    public static void AddApplication(this IServiceCollection service)
    {
        service.AddTransient<IFileService, FileService>();
        service.AddScoped<ICustomerImplementation, CustomerImplementation>();
        service.AddScoped<IAuthenticationImplementaton, AuthenticationImplementation>();
        service.AddScoped<IAddressImplementation, AddressImplementation>();
    }
}
