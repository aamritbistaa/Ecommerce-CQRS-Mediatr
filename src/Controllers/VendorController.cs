using CQRSApplication.Command.VendorCommand;
using CQRSApplication.Query.VendorQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace CQRSApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VendorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VendorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new GetAllVendorQuery(), cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to get all Vendors: {@message}", ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{VendorId:guid}")]
        public async Task<IActionResult> GetById(Guid VendorId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new GetVendorById { VendorId = VendorId }, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Error encountered while getting the Vendor of {@Id} \n{@message}", VendorId, ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // [HttpPost]
        // [Authorize(Roles = "Vendor,SuperAdmin")]
        // public async Task<IActionResult> Create(CreateVendorCommand command, CancellationToken cancellationToken)
        // {
        //     try
        //     {
        //         var response = await _mediator.Send(command, cancellationToken);
        //         return Ok(response);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //     }
        // }

        [HttpPut]
        [Authorize(Roles = "Vendor,SuperAdmin")]
        public async Task<IActionResult> Update(UpdateVendorCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(command, cancellationToken);
                Log.Information("Vendor {@Id} updated successfully", command.VendorId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to upadate Vendor {@Id} /n{@message}", command.VendorId, ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{Id:guid}")]
        [Authorize(Roles = "Vendor,SuperAdmin")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new DeleteVendorCommand { VendorId = Id }, cancellationToken);
                Log.Information("Vendor {@Id} deleted successfully", Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(messageTemplate: "Unable to delete Vendor {@Id} \n {@message}", Id, ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
