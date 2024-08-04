using CQRS.Ecommerce.Application;
using CQRS.Ecommerce.Application.Features.Products.Requests;
using CQRS.Ecommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Ecommerce.Api;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("GetAllProduct")]
    public async Task<ServiceResult<List<Product>>> GetAllProduct()
    {
        var result = await _mediator.Send(new GetAllProductQuery { });
        return result;
    }
    [HttpGet("GetProductById/{id:guid}")]
    public async Task<ServiceResult<Product>> GetProductById([FromRoute] Guid id)
    {
        var request = new GetProductByIdQuery
        {
            Id = id,
        };
        var result = await _mediator.Send(request);
        return result;
    }

    [HttpPost("CreateProduct")]
    public async Task<ServiceResult<bool>> CreateProduct(CreateProductCommand request)
    {
        var result = await _mediator.Send(request);
        return result;
    }
    [HttpPut("UpdateProduct")]
    public async Task<ServiceResult<bool>> UpdateProduct(UpdateProductCommand request)
    {
        var result = await _mediator.Send(request);
        return result;
    }
    [HttpPut("DeleteProduct")]
    public async Task<ServiceResult<bool>> DeleteProduct(DeleteProductCommand request)
    {
        var result = await _mediator.Send(request);
        return result;
    }
    [HttpDelete("PermaDeleteProduct")]
    public async Task<ServiceResult<bool>> PermaDeleteProduct(PermaDeleteProductCommand request)
    {
        var result = await _mediator.Send(request);
        return result;
    }
}
