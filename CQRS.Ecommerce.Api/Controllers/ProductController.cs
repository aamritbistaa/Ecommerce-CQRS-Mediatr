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
        var result = await _mediator.Send(new GetAllProductRequest { });
        return result;
    }
    [HttpGet("GetProductById")]
    public async Task<IActionResult> GetProductById()
    {
        return Ok();
    }

    [HttpPost("CreateProduct")]
    public async Task<ServiceResult<bool>> CreateProduct(CreateProductCommand request)
    {
        var result = await _mediator.Send(request);
        return result;
    }
    [HttpPut("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct()
    {
        return Ok();
    }
    [HttpDelete("DeleteProduct")]
    public async Task<IActionResult> DeleteProduct()
    {
        return Ok();
    }

}
