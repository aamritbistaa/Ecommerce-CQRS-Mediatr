using CQRS.Ecommerce.Domain;
using MediatR;

namespace CQRS.Ecommerce.Application;

public class PermaDeleteProductCommandHandler : IRequestHandler<PermaDeleteProductCommand, ServiceResult<bool>>
{
    private readonly IProductService _service;

    public PermaDeleteProductCommandHandler(IProductService service)
    {
        _service = service;
    }

    public async Task<ServiceResult<bool>> Handle(PermaDeleteProductCommand request, CancellationToken cancellationToken)
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
        var result = await _service.DeleteProductAsync(item);

        if (result is true)
        {
            return new ServiceResult<bool>
            {
                StatusCode = StatusCode.Success,
                Message = Message.SuccessWhileDeleting,
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
