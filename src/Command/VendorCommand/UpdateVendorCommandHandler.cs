using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Command.VendorCommand
{
    public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, Vendor>
    {
        private readonly CQRSDbContext _dbContext;
        public UpdateVendorCommandHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Vendor> Handle(UpdateVendorCommand command, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Vendors
                        .FirstOrDefaultAsync(v => v.Id == command.VendorId);
            if (item == null)
            {
                throw new Exception(message: "Vendor does not exist");
            }
            item.ShopName = command.ShopName;
            item.ShopAddress = command.ShopAddress;
            item.PanNo = command.PanNo;

            await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}
