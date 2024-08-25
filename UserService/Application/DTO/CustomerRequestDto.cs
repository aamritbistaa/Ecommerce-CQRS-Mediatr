using Microsoft.AspNetCore.Http;
using System;
using System.Buffers.Text;

namespace Application.DTO;

public class CreateCustomerRequestDto
{
    public IFormFile ProfilePicture { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
public class UpdateCustomerRequestDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}