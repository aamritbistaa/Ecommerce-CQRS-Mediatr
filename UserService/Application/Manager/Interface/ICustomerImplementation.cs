using System;
using Application.Common;
using Application.DTO;
using static Application.Manager.Implementation.CustomerImplementation;

namespace Application.Manager.Interface;

public interface ICustomerImplementation
{
    Task<ServiceResult<ListResponseDto<List<CustomerResponse>>>> ListAllCustomer(CustomerListRequestFilter filter);
    Task<ServiceResult<CustomerResponse>> GetCustomerById(Guid id);
    Task<ServiceResult<Guid?>> CreateCustomer(CreateCustomerRequestDto request);
    Task<ServiceResult<bool>> UpdateCustomer(UpdateCustomerRequestDto request);
    Task<ServiceResult<bool>> DeleteCustomer(Guid id);
}
