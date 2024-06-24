using CQRSApplication.Context;
using CQRSApplication.Helpers;
using CQRSApplication.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSApplication.Command.AuthUserCommand
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly CQRSDbContext _dbContext;
        private readonly IMediator _mediator;
        public UpdateUserCommandHandler(CQRSDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task<User> Handle([FromForm] UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _dbContext.Users.FindAsync(request.Id);
            if (existingUser == null)
            {
                throw new Exception(message: "User does not exist");
            }
            //Todo: Validate request.id == token.claims.id or throw exception of user has no permission
            var imageUrl = existingUser.ImageUrl;
            if (request.Image != null)
            {
                imageUrl = await _mediator.Send(new UploadFileCommand
                {
                    Genre = "User",
                    file = request.Image,
                });

                if (!(existingUser.ImageUrl == null || existingUser.ImageUrl == "" || existingUser.ImageUrl == "string"))
                {
                    await _mediator.Send(new DeleteFileCommand
                    {
                        Genre = "User",
                        fileNameWithExtension = existingUser.ImageUrl,
                    });
                }
            }

            existingUser.FirstName = request.FirstName;
            existingUser.LastName = request.LastName;
            existingUser.PhoneNumber = request.PhoneNumber;
            existingUser.Address = request.Address;
            existingUser.ImageUrl = imageUrl;

            var existingUserShippingAddress = await _dbContext.ShippingAddresses.FindAsync(existingUser.ShippingAddressId);
            if (existingUserShippingAddress == null)
            {
                //create new shipping address
                var newShippingAddress = new ShippingAddress
                {
                    Id = Guid.NewGuid(),
                    District = request.ShippingAddress.District,
                    City = request.ShippingAddress.City,
                    StreetAddress = request.ShippingAddress.Street
                };
                await _dbContext.ShippingAddresses.AddAsync(newShippingAddress);
                existingUser.ShippingAddressId = newShippingAddress.Id;
            }

            var existingUserCredentials = await _dbContext.UserCredentials.FindAsync(existingUser.UserCredentialsId);
            if (existingUserCredentials == null)
            {
                throw new Exception(message: "UserCredentials not found");
            }
            /* if (request.UpdateUserCredentials.NewPassword != null)*/
            if (!string.IsNullOrWhiteSpace(request.UpdateUserCredentials.NewPassword))
            {
                if (existingUserCredentials.Password == request.UpdateUserCredentials.OldPassword)
                {
                    existingUserCredentials.Password = request.UpdateUserCredentials.NewPassword;
                }
                else
                {
                    throw new Exception(message: "Old password is incorrect");
                }
            }
            existingUserCredentials.UserName = request.UpdateUserCredentials.UserName;
            existingUserCredentials.Email = request.UpdateUserCredentials.Email;
            await _dbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}
