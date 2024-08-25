using System;
using Application.DTO;
using Domain.Entity;

namespace Application.Mapper;

public static class CustomerMapper
{
    public static CustomerResponse ECustomerToCustomerResponseMapper(ECustomer request)
    {
        string base64ImageRepresentation = "";
        try
        {
            if (request.ProfilePicture is not null)
            {
                byte[] imageArray = File.ReadAllBytes($"Resources/{request.ProfilePicture}");
                base64ImageRepresentation = Convert.ToBase64String(imageArray);
            }
        }
        catch (System.Exception)
        {

        }

        return new CustomerResponse
        {
            CustomerId = request.Id,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            ProfilePicUrl = request.ProfilePicUrl,
            DateOfBirth = request.DateOfBirth,
            ImageBase64Reperesentation = base64ImageRepresentation
        };

    }


}
