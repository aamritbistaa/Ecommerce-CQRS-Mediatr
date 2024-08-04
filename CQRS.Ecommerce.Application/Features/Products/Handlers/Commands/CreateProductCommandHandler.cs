using CQRS.Ecommerce.Domain;
using CQRS.Ecommerce.Domain.Entities;
using MediatR;

namespace CQRS.Ecommerce.Application;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResult<bool>>
{
    private readonly IProductService _service;

    public CreateProductCommandHandler(IProductService service)
    {
        _service = service;
    }

    public async Task<ServiceResult<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var item = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Category = request.Category,
            Stock = request.Stock,
            VendorId = request.VendorId,
        };
        var result = await _service.CreateProduct(item);

        if (result is true)
        {
            return new ServiceResult<bool>
            {
                StatusCode = StatusCode.Success,
                Message = Message.SuccessWhileCreating,
                Data = result
            };
        }
        return new ServiceResult<bool>
        {
            StatusCode = StatusCode.ServerError,
            Message = Message.ErrorWhileCreating,
            Data = result
        };
    }
}
