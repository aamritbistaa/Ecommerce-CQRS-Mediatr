using CQRSApplication.Context;
using CQRSApplication.Helpers;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Command.AuthUserCommand
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, User>
    {
        private readonly CQRSDbContext _dbContext;
        private readonly IMediator _mediator;
        public DeleteUserCommandHandler(CQRSDbContext dbContext,
                                        IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Users
                .Include(c => c.userCredentials)
                .Include(c => c.shippingAddress)
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (item == null)
            {
                throw new Exception(message: "User does not exist");
            }
            //Todo: Validate request.id == token.claims.id or throw exception of user has no permission
            if (item.userCredentials.Role == RoleType.SuperAdmin)
            {
                throw new Exception(message: "You dont have permission to execute this operation");
            }
            if (item.userCredentials.Email != request.Email || item.userCredentials.Password != request.Password)
            {
                throw new Exception(message: "Invalid User Credentials");
            }
            //if (!string.IsNullOrWhiteSpace(item.ImageUrl))
            if (item.ImageUrl != null || item.ImageUrl != "" || item.ImageUrl != "string")
            {
                await _mediator.Send(new DeleteFileCommand
                {
                    Genre = "User",
                    fileNameWithExtension = item.ImageUrl
                });
                // _fileServices.DeleteFile("User", item.ImageUrl);
            }
            _dbContext.Users.Remove(item);
            // var credentialItem = await _dbContext.UserCredentials.FirstOrDefaultAsync(c => c.Id == item.UserCredentialsId);
            // _dbContext.UserCredentials.Remove(credentialItem);
            // var shippingItem = await _dbContext.ShippingAddresses.FirstOrDefaultAsync(c => c.Id == item.ShippingAddressId);
            // _dbContext.ShippingAddresses.Remove(shippingItem);

            _dbContext.UserCredentials.Remove(item.userCredentials);
            _dbContext.ShippingAddresses.Remove(item.shippingAddress);
            await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}
