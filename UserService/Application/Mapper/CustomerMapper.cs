using System;
using Application.DTO;
using Domain.Entity;

namespace Application.Mapper;

public static class CustomerMapper
{
    public static CustomerResponse ECustomerToCustomerResponseMapper(ECustomer request)
    {
        return new CustomerResponse
        {
            CustomerId = request.Id,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            ProfilePicUrl = request.ProfilePicUrl,
            DateOfBirth = request.DateOfBirth,
        };

    }

}
