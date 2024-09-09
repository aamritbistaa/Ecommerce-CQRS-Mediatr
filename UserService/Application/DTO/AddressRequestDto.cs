using System;

namespace Application.DTO;

public class CreateCountryRequestDto
{
    public string CountryName { get; set; }
    public string? CountryCode { get; set; }

}
public class UpdateCountryRequestDto
{
    public Guid Id { get; set; }
    public string CountryName { get; set; }
    public string? CountryCode { get; set; }
}
