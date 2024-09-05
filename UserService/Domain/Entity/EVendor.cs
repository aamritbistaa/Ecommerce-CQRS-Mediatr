using System;
using Domain.Common;
using Domain.Enum;

namespace Domain.Entity;

public class EVendor : BaseEntity
{
    public string ShopName { get; set; }
    public string ShopLogoUrl { get; set; } = string.Empty;
    public string RegistrtionNo { get; set; }
    public ShopSize ShopSize { get; set; }
    public UserStatus UserStatus { get; set; } = UserStatus.Unverified;
}
