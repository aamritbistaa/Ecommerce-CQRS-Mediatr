using Application.Common;
using Application.DTO;
using Application.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerImplementation _customerManager;

        public CustomerController(ICustomerImplementation customerManager)
        {
            _customerManager = customerManager;
        }
        [HttpGet("Test")]
        public string Test()
        {
            return "Hello";
        }
        [HttpGet("GetAllCustomer")]
        public async Task<ServiceResult<List<CustomerResponse>>> GetAllCustomer()
        {
            var response = await _customerManager.ListAllCustomer();
            return response;
        }
        [HttpGet("GetCustomerById")]
        public async Task<ServiceResult<CustomerResponse>> GetCustomerById(Guid id)
        {
            var response = await _customerManager.GetCustomerById(id);
            return response;
        }
        [HttpPost("CreateCustomer")]
        public async Task<ServiceResult<Guid?>> CreateCustomer([FromForm] CreateCustomerRequestDto request)
        {
            var response = await _customerManager.CreateCustomer(request);
            return response;
        }
        [HttpPut("UpdateCustomer")]
        public async Task<ServiceResult<bool>> UpdateCustomer(UpdateCustomerRequestDto request)
        {
            var response = await _customerManager.UpdateCustomer(request);
            return response;
        }
        [HttpDelete("DeleteCustomer")]
        public async Task<ServiceResult<bool>> DeleteCustomer(Guid id)
        {
            var response = await _customerManager.DeleteCustomer(id);
            return response;
        }
    }
}
