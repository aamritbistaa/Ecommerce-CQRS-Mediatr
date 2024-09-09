using System;
using Application.Common;
using Application.DTO;

namespace Application.Manager.Interface;

public interface IAddressImplementation
{
    Task<ServiceResult<List<CountryResponseDto>>> ListAllCountry();
    Task<ServiceResult<Guid?>> AddCountry(CreateCountryRequestDto request);
    Task<ServiceResult<bool>> UpdateCountry(UpdateCountryRequestDto request);
}
