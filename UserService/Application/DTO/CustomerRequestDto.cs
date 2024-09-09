using Application.Common;
using Domain.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Buffers.Text;

namespace Application.DTO;

public class CustomerListRequestFilter
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
    [CustomDateValidation(true)]
    public string? CreatedDateLowerLimit { get; set; }
    [CustomDateValidation(true)]
    public string? CreatedDateUpperLimit { get; set; }
    public SortingRequestFilter? Sort { get; set; }
}
public class CreateCustomerRequestDto
{
    public IFormFile ProfilePicture { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    [CustomDateValidation(false)]
    public string DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string StreetAddress { get; set; }
}
public class UpdateCustomerRequestDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    [CustomDateValidation]
    public string DateOfBirth { get; set; }
}