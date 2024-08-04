using CQRS.Ecommerce.Domain;
using MediatR;

namespace CQRS.Ecommerce.Application;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ServiceResult<bool>>
{
    private readonly IProductService _service;

    public DeleteProductCommandHandler(IProductService service)
    {
        _service = service;
    }

    public async Task<ServiceResult<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
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
        item.IsDeleted = true;
        item.UpdatedAt = DateTime.UtcNow;
        var result = await _service.UpdateProductAsync(item);

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
