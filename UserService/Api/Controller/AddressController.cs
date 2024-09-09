using Application.Common;
using Application.DTO;
using Application.Manager.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressImplementation _manager;

        public AddressController(IAddressImplementation manager)
        {
            _manager = manager;
        }
        [HttpGet("ListAllCountry")]
        public async Task<ServiceResult<List<CountryResponseDto>>> ListAllCountry()
        {
            var response = await _manager.ListAllCountry();
            return response;
        }
        [HttpPost("AddCountry")]
        public async Task<ServiceResult<Guid?>> AddCountry(CreateCountryRequestDto request)
        {
            var response = await _manager.AddCountry(request);
            return response;
        }
        [HttpPut("UpdateCountry")]
        public async Task<ServiceResult<bool>> UpdateCountry(UpdateCountryRequestDto request)
        {
            var response = await _manager.UpdateCountry(request);
            return response;
        }
    }
}
