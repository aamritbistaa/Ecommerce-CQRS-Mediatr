using System;
using Application.Common;
using Application.DTO;
using Application.Manager.Interface;
using Domain.Entity;
using Domain.Interface;

namespace Application.Manager.Implementation;

public class AddressImplementation : IAddressImplementation
{
    private readonly ICountryService _countryService;

    public AddressImplementation(ICountryService countryService)
    {
        _countryService = countryService;
    }
    public async Task<ServiceResult<List<CountryResponseDto>>> ListAllCountry()
    {
        var response = await _countryService.ListAllAsync();
        var data = (from item in response
                    select new CountryResponseDto
                    {
                        Id = item.Id,
                        CountryName = item.CountryName,
                        CountryCode = item.CountryCode,
                    }
        ).ToList();
        return new ServiceResult<List<CountryResponseDto>>
        {
            StatusCode = StatusCode.Success,
            Message = Messages.Success,
            Data = data
        };
    }
    public async Task<ServiceResult<Guid?>> AddCountry(CreateCountryRequestDto request)
    {

        var data = new ECountryAddress
        {
            CountryName = request.CountryName,
            CountryCode = request?.CountryCode
        };
        var response = await _countryService.AddItemAsync(data);
        return new ServiceResult<Guid?>
        {
            StatusCode = StatusCode.Created,
            Message = $"{Messages.Created} Displaying Id :",
            Data = response.Id
        };
    }
    public async Task<ServiceResult<bool>> UpdateCountry(UpdateCountryRequestDto request)
    {
        var country = await _countryService.GetDataAsync(request.Id);
        if (country == null)
        {
            return new ServiceResult<bool>
            {
                StatusCode = StatusCode.NoContent,
                Message = Messages.NoContent,
                Data = false
            };
        }
        country.CountryName = request.CountryName;
        country.CountryCode = request.CountryCode;
        country.UpdatedDate = DateTime.Now;
        var result = await _countryService.UpdateItemAsync(country);
        if (result == false)
        {
            return new ServiceResult<bool>
            {
                StatusCode = StatusCode.ServerError,
                Message = Messages.ServerError,
                Data = false
            };
        }
        return new ServiceResult<bool>
        {
            StatusCode = StatusCode.Success,
            Message = Messages.Success,
            Data = true
        };
    }

}
