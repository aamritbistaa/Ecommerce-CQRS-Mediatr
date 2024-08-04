using CQRS.Ecommerce.Domain;
using MediatR;

namespace CQRS.Ecommerce.Application;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResult<bool>>
{
    private readonly IProductService _service;

    public UpdateProductCommandHandler(IProductService service)
    {
        _service = service;
    }

    public async Task<ServiceResult<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var item = await _service.GetProductByIdAsync(request.Id);


        if (item == null)
        {
            return new ServiceResult<bool>
            {
                StatusCode = StatusCode.ClientError,
                Message = Message.NotFound,
                Data = false
            };
        }

        item.Name = request.Name;
        item.Description = request.Description;
        item.Price = request.Price;
        item.Category = request.Category;
        item.Stock = request.Stock;
        item.VendorId = request.VendorId;
        item.UpdatedAt = DateTime.UtcNow;

        var result = await _service.UpdateProductAsync(item);

        if (result is true)
        {
            return new ServiceResult<bool>
            {
                StatusCode = StatusCode.Success,
                Message = Message.SuccessWhileUpdaing,
                Data = result
            };
        }
        return new ServiceResult<bool>
        {
            StatusCode = StatusCode.ServerError,
            Message = Message.ErrorWhileDeleting,
            Data = result
        };
    }
}
