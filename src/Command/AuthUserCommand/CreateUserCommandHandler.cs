using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CQRSApplication.Context;
using CQRSApplication.Helpers;
using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.AuthUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        public readonly CQRSDbContext _dbContext;
        private readonly IMediator _mediator;
        public CreateUserCommandHandler(CQRSDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task<User> Handle([FromForm] CreateUserCommand request,
                                    CancellationToken cancellationToken)
        {
            var newUserCredentials = new UserCredentials
            {
                Id = Guid.NewGuid(),
                Email = request.UserCredentials.Email,
                UserName = request.UserCredentials.UserName,
                Password = request.UserCredentials.Password,
                IsActive = true
            };
            //Check if Usercredential already exists.
            if (await _dbContext.UserCredentials.AnyAsync
                    (uc => uc.Email == newUserCredentials.Email ||
                    uc.UserName == newUserCredentials.UserName,
                    cancellationToken))
            {
                throw new Exception("User name or email already exists with the same name");
            }
            await _dbContext.UserCredentials.AddAsync(newUserCredentials, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var shippingAdd = new ShippingAddress
            {
                Id = Guid.NewGuid(),
                District = request.ShippingDetails.District,
                City = request.ShippingDetails.City,
                StreetAddress = request.ShippingDetails.StreetAddress,
            };
            await _dbContext.ShippingAddresses.AddAsync(shippingAdd);
            await _dbContext.SaveChangesAsync();
            //ImageUrl initially as empty.
            string imageUrl = string.Empty;
            if (request.Image != null)
            {
                // ImageUrl = await _fileServices.UploadFile("User", request.Image);
                imageUrl = await _mediator.Send(new UploadFileCommand
                {
                    file = request.Image,
                    Genre = "User"
                });
            }

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                ImageUrl = imageUrl,
                UserCredentialsId = newUserCredentials.Id,
                ShippingAddressId = shippingAdd.Id,
            };
            await _dbContext.Users.AddAsync(newUser, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newUser;
        }
    }
}
