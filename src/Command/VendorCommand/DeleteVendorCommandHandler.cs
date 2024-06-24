using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.VendorCommand
{
    public class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, Vendor>
    {
        private readonly CQRSDbContext _dbContext;
        public DeleteVendorCommandHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Vendor> Handle(DeleteVendorCommand command, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Vendors
                        .FindAsync(command.VendorId);
            if (item == null)
            {
                throw new Exception(message: "Cannot find the specified vendor");
            }
            if (item.UserId == command.UserId)
            {
                throw new Exception(message: "Invalid User Id");
            }
            var user = await _dbContext.Users.FindAsync(command.UserId);
            if (user == null)
            {
                throw new Exception("Cant find the user");
            }
            _dbContext.Vendors.Remove(item);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}
