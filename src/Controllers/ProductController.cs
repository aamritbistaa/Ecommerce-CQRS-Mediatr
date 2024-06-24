using CQRSApplication.Command.ProductCommand;
using CQRSApplication.Query.ProductQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CQRSApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new GetAllProductQuery(), cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to get all Products: {@message}", ex.Message);
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }

        [HttpGet]
        [Route("{Id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new GetProductQuery { Id = Id }, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Error encountered while getting the Product of {@Id} \n{@message}", Id, ex.Message);
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> Create([FromForm] CreateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(command, cancellationToken);
                Log.Information("Product {@Id} created successfully", response.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Vendor {@VendorId} was unable to create Product \n{Message}", command.VendorId, ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPut]
        [Authorize(Roles = "SuperAdmin,Vendor")]
        public async Task<IActionResult> Update([FromForm] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(command, cancellationToken);
                Log.Information("Product {@Id} updated successfully", command.ProductId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to upadate Product {@Id} /n{@message}", command.ProductId, ex.Message);
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "SuperAdmin,Vendor")]
        public async Task<IActionResult> Delete(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(command, cancellationToken);
                Log.Information("Product {@Id} deleted successfully", command.ProductId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(messageTemplate: "Unable to delete product {@Id} \n {@message}", command.ProductId, ex.Message);
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }
    }
}
