using System;
using Domain.Common;

namespace Domain.Entity;

public class EUser : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid UserCredentialsId { get; set; }
}
