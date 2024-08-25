using Application.Common;
using Application.DTO;
using Application.Helper;
using Application.Manager.Interface;
using Application.Mapper;
using Domain.Entity;
using Domain.Enum;
using Domain.Interface;

namespace Application.Manager.Implementation;
public class CustomerImplementation : ICustomerImplementation
{
    private readonly ICustomerService _customerService;
    private readonly IUserCredentialsService _userCredentialsService;
    private readonly IUserService _userService;
    private readonly IServiceFactory _factory;
    private readonly IFileService _fileService;
    public CustomerImplementation(ICustomerService customerService, IUserCredentialsService userCredentialsService, IUserService userService, IServiceFactory factory, IFileService fileService)
    {
        _customerService = customerService;
        _userCredentialsService = userCredentialsService;
        _userService = userService;
        _factory = factory;
        _fileService = fileService;
    }
    public async Task<ServiceResult<List<CustomerResponse>>> ListAllCustomer()
    {
        var customers = await _customerService.ListAllAsync();
        if (customers.Count < 1)
        {
            return new ServiceResult<List<CustomerResponse>>
            {
                StatusCode = StatusCode.NoContent,
                Message = Messages.NoContent,
                Data = new List<CustomerResponse>()
            };
        }
        var customerResponse = (from item in customers select CustomerMapper.ECustomerToCustomerResponseMapper(item)).ToList();
        return new ServiceResult<List<CustomerResponse>>()
        {
            StatusCode = StatusCode.Success,
            Message = Messages.Success,
            Data = customerResponse,
        };
    }
    public async Task<ServiceResult<CustomerResponse>> GetCustomerById(Guid id)
    {
        var customer = await _customerService.GetDataAsync(id);
        if (customer == null)
        {
            return new ServiceResult<CustomerResponse>
            {
                StatusCode = StatusCode.NoContent,
                Message = Messages.NoContent,
                Data = new CustomerResponse()
            };
        }
        var customerResponse = CustomerMapper.ECustomerToCustomerResponseMapper(customer);
        return new ServiceResult<CustomerResponse>()
        {
            StatusCode = StatusCode.Success,
            Message = Messages.Success,
            Data = customerResponse,
        };
    }
    public async Task<ServiceResult<Guid?>> CreateCustomer(CreateCustomerRequestDto request)
    {
        //! Validation
        #region Validation
        var dataWithSameEmail = await _userCredentialsService.GetByEmail(request.Email);
        if (dataWithSameEmail != null)
        {
            return new ServiceResult<Guid?>
            {
                StatusCode = StatusCode.RequestClash,
                Message = Messages.RequestClash,
                Data = null
            };
        }
        #endregion

        try
        {
            _factory.BeginTransaction();
            var customerCredentials = new EUserCredentials()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Password = request.Password,
                LastActive = DateTime.Now,
                Role = UserRole.Customer,
            };
            var customerCredentialsResponse = await _userCredentialsService.AddItemAsync(customerCredentials);

            var imageName = await _fileService.UploadFileAsync(request.ProfilePicture);

            var customer = new ECustomer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                DateOfBirth = request.DateOfBirth,
                ProfilePicture = imageName
            };
            var customerResponse = await _customerService.AddItemAsync(customer);
            var user = new EUser()
            {
                UserId = customerResponse.Id,
                UserCredentialsId = customerCredentialsResponse.Id,
            };
            var userResponse = await _userService.AddItemAsync(user);
            _factory.Commit();
            return new ServiceResult<Guid?>
            {
                StatusCode = StatusCode.Created,
                Message = $"{Messages.Created} Displaying your userId is",
                Data = userResponse.Id
            };
        }
        catch (Exception ex)
        {
            _factory.RollBack();
            return new ServiceResult<Guid?>
            {
                StatusCode = StatusCode.ServerError,
                Message = $"{Messages.ServerError} {ex.Message}",
                Data = null
            };
        }
    }
    public async Task<ServiceResult<bool>> UpdateCustomer(UpdateCustomerRequestDto request)
    {
        var customer = await _customerService.GetDataAsync(request.Id);
        if (customer == null)
        {
            return new ServiceResult<bool>
            {
                StatusCode = StatusCode.NoContent,
                Message = Messages.NoContent,
                Data = false
            };
        }
        customer.UpdatedDate = DateTime.Now;
        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.MiddleName = request.MiddleName;
        var response = await _customerService.UpdateItemAsync(customer);
        if (response == true)
        {
            return new ServiceResult<bool>
            {
                StatusCode = StatusCode.Success,
                Message = Messages.Success,
                Data = true
            };
        }
        return new ServiceResult<bool>
        {
            StatusCode = StatusCode.ServerError,
            Message = Messages.ServerError,
            Data = false
        };
    }
    public async Task<ServiceResult<bool>> DeleteCustomer(Guid id)
    {
        var customer = await _customerService.GetDataAsync(id);
        if (customer == null)
        {
            return new ServiceResult<bool>
            {
                StatusCode = StatusCode.NoContent,
                Message = Messages.NoContent,
                Data = false
            };
        }
        customer.UpdatedDate = DateTime.Now;
        customer.IsDeleted = true;
        var response = await _customerService.UpdateItemAsync(customer);
        if (response == true)
        {
            return new ServiceResult<bool>
            {
                StatusCode = StatusCode.Success,
                Message = Messages.Success,
                Data = true
            };
        }
        return new ServiceResult<bool>
        {
            StatusCode = StatusCode.ServerError,
            Message = Messages.ServerError,
            Data = false
        };
    }
}
