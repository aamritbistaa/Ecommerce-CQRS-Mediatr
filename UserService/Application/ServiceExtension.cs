using System;
using Application.Manager.Implementation;
using Application.Manager.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceExtension
{
    public static void AddApplication(this IServiceCollection service)
    {
        service.AddScoped<ICustomerImplementation, CustomerImplementation>();
    }

}
