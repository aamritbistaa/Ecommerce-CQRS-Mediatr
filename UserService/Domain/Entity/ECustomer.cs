using System;
using Domain.Common;

namespace Domain.Entity;

public class ECustomer : BaseEntity
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicUrl { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}
