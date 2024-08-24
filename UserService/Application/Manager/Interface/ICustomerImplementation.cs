using System;
using Application.Common;
using Application.DTO;

namespace Application.Manager.Interface;

public interface ICustomerImplementation
{
    Task<ServiceResult<List<CustomerResponse>>> ListAllCustomer();
    Task<ServiceResult<CustomerResponse>> GetCustomerById(Guid id);
    Task<ServiceResult<Guid?>> CreateCustomer(CreateCustomerRequestDto request);
    Task<ServiceResult<bool>> UpdateCustomer(UpdateCustomerRequestDto request);
    Task<ServiceResult<bool>> DeleteCustomer(Guid id);
}
