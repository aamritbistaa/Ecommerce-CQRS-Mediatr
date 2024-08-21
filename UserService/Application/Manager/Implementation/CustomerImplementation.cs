using System;
using Application.DTO;
using Domain.Entity;
using Domain.Enum;

namespace Application.Manager.Implementation;

public class CustomerImplementation
{
    public async Task GetAllCustomer()
    {

    }
    public async Task GetCustomerById(int id)
    {

    }
    public async Task CreateCustomer(CreateCustomerRequestDto request)
    {
        var customerCredentials = new EUserCredentials()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Password = request.Password,
            LastActive = DateTime.Now,
            Role = UserRole.Customer,
        };
        var customer = new ECustomer()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            DateOfBirth = request.DateOfBirth,
        };
        var user = new EUser()
        {
            UserId = customer.Id,
            UserCredentialsId = customerCredentials.Id,
        };


    }
    public async Task UpdateCustomer()
    {

    }
    public async Task DeleteCustomer()
    {

    }
}
