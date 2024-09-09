using System;

namespace Application.DTO;

public class AddressResponseDto
{

}

public class CountryResponseDto
{
    public Guid Id { get; set; }
    public string CountryName { get; set; }
    public string? CountryCode { get; set; }

}
