using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Command.VendorCommand
{
    public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, Vendor>
    {
        private readonly CQRSDbContext _dbContext;
        public CreateVendorCommandHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Vendor> Handle(CreateVendorCommand command, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                        .FindAsync(command.UserId);
            if (user == null)
            {
                throw new Exception("Unable to find User");

            }
            var cred = await _dbContext.UserCredentials
                        .FirstOrDefaultAsync(c => c.Id == user.UserCredentialsId);

            if (cred == null)
            {
                throw new Exception("Unable to find Credential for the user");

            }
            if (cred.Role != RoleType.Vendor)
            {
                throw new Exception("Sorry you are not registered as Vendor");
            }
            var vendor = new Vendor
            {
                Id = Guid.NewGuid(),
                ShopAddress = command.ShopAddress,
                ShopName = command.ShopName,
                PanNo = command.PanNo,
                UserId = command.UserId,
            };

            await _dbContext.Vendors.AddAsync(vendor);
            await _dbContext.SaveChangesAsync();
            return vendor;
        }
    }
}
