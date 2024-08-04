
using CQRS.Ecommerce.Domain;
using CQRS.Ecommerce.Domain.Entities;
using MediatR;

namespace CQRS.Ecommerce.Application;

public class GetProductByIdRequestHandler : IRequestHandler<GetProductByIdQuery, ServiceResult<Product>>
{
    private readonly IProductService _service;

    public GetProductByIdRequestHandler(IProductService productService)
    {
        _service = productService;
    }

    public async Task<ServiceResult<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _service.GetProductByIdAsync(request.Id);
        if (data != null)
        {

            return new ServiceResult<Product>
            {
                StatusCode = StatusCode.Success,
                Message = Message.Success,
                Data = data,
            };
        }
        return new ServiceResult<Product>
        {
            StatusCode = StatusCode.Success,
            Message = Message.Null,
            Data = new Product()
        };
    }
}
