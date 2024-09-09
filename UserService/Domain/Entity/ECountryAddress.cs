using System;
using Domain.Common;

namespace Domain.Entity;

public class ECountryAddress : BaseEntity
{
    public string CountryName { get; set; }
    public string? CountryCode { get; set; }
}
