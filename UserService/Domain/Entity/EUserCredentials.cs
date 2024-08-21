using System;
using Domain.Common;
using Domain.Enum;

namespace Domain.Entity;

public class EUserCredentials : BaseEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime LastActive { get; set; }
    public UserRole Role { get; set; }
}
