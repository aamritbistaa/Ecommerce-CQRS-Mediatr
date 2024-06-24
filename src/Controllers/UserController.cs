using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRSApplication.Command.AuthUserCommand;
using CQRSApplication.Query.UserQuery;
using Microsoft.EntityFrameworkCore;
using CQRSApplication.Context;
using CQRSApplication.Model;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace CQRSApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly CQRSDbContext _dbContext;
        private readonly IMediator _mediator;
        public UserController(IMediator mediator, CQRSDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(new GetAllUserQuery(), cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to get all Users: {@message}", ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery { Id = Id };
            try
            {
                var response = await _mediator.Send(query, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Error encountered while getting the User of {@Id} \n{@message}", Id, ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin,Vendor,Customer")]
        public async Task<IActionResult> Update([FromForm] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(command, cancellationToken);
                Log.Information("User {@Id} updated successfully", command.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to upadate User {@Id} /n{@message}", command.Id, ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "SuperAdmin,Vendor,Customer")]
        public async Task<IActionResult> Delete(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(command, cancellationToken);
                Log.Information("User {@Id} deleted successfully", command.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(messageTemplate: "Unable to delete User {@Id} \n {@message}", command.Id, ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ChangeToVendor(Guid Id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(u => u.userCredentials)
                .Include(u => u.shippingAddress)
                .FirstOrDefaultAsync(c => Id == c.Id);
            if (user == null)
            {
                Log.Error(messageTemplate: "Unable to find User {@Id}", Id);
                throw new Exception(message: "User with specified Id does not exist");
            }
            if (user.userCredentials.Role == RoleType.Customer)
            {
                user.userCredentials.Role = RoleType.Vendor;
                var address = $"{user.shippingAddress.StreetAddress},{user.shippingAddress.City},{user.shippingAddress.District} ";
                if (user.CartId != null)
                {
                    var itemToDelete = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == user.CartId);
                    if (itemToDelete != null)
                    {
                        _dbContext.Carts.Remove(itemToDelete);
                        user.ShippingAddressId = null;
                        await _dbContext.SaveChangesAsync();
                    }
                }
                Vendor vendor = new Vendor
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    ShopName = "Default Shop",
                    ShopAddress = address,
                    PanNo = " Default Pan Number",
                };
                _dbContext.Vendors.Add(vendor);
                await _dbContext.SaveChangesAsync();
                Log.Information(messageTemplate: "User {@Id} updated", Id);
            }
            else if (user.userCredentials.Role == RoleType.Vendor)
            {
                Log.Information("User {@Id} is alredy a vendor", Id);
                throw new Exception(message: "Unable to assign 'Vendor' to a vendor");
            }
            else
            {
                Log.Information("User {@Id} is not a vendor or customer", Id);
                throw new Exception(message: "You don't have permission to do that");
            }
            return Ok($"Changed {user.userCredentials.UserName} to vendor");
        }
        [HttpPut]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ChangeToMember(Guid Id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                        .Include(u => u.userCredentials)
                        .FirstOrDefaultAsync(c => Id == c.Id);
            if (user == null)
            {
                Log.Error(messageTemplate: "Unable to find User {@Id}", Id);
                throw new Exception(message: "User with specified Id does not exist");
            }
            if (user.userCredentials.Role == RoleType.Vendor)
            {
                user.userCredentials.Role = RoleType.Customer;
                var vendor = await _dbContext.Vendors
                            .FirstOrDefaultAsync(v => v.UserId == user.Id, cancellationToken);
                if (vendor != null)
                {
                    _dbContext.Vendors.Remove(vendor);
                }

                await _dbContext.SaveChangesAsync();
                Log.Information(messageTemplate: "User {@Id} updated", Id);
            }
            else if (user.userCredentials.Role == RoleType.Customer)
            {
                Log.Information("User {@Id} is alredy a Customer", Id);
                throw new Exception(message: "Unable to assign 'Customer' to a customer");
            }
            else
            {
                Log.Information("User {@Id} is not a vendor or customer", Id);
                throw new Exception(message: "You don't have permission to do that");
            }
            return Ok($"Changed {user.userCredentials.UserName} to customer");
        }
    }
}
