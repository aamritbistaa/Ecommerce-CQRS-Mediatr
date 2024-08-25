using System;

namespace Application.DTO;

public class CustomerResponse
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePicUrl { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string ImageBase64Reperesentation { get; set; }
}
