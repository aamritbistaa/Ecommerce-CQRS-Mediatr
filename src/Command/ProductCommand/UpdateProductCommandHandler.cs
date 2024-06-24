using CQRSApplication.Context;
using CQRSApplication.Helpers;
using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.ProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly CQRSDbContext _dbContext;
        private readonly IMediator _mediator;
        public UpdateProductCommandHandler(CQRSDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task<Product> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Products
                        .FindAsync(new Guid(command.ProductId));
            if (item == null)
            {
                throw new Exception(message: "Item not found");
            }
            var vendor = await _dbContext.Vendors
                        .FindAsync(command.VendorId);
            if (vendor == null)
            {
                throw new Exception("Vendor does not exist in current context");

            }
            if (command.VendorId != vendor.Id)
            {
                throw new Exception(message: "You dont have privilage to delete this item");
            }
            //Todo:Execute only if item.UserId == USERID()

            string oldImage = item.ImageUrl;
            if (command.Image != null)
            {
                // item.ImageUrl = await _fileServices.UploadFile("Product", command.Image);
                item.ImageUrl = await _mediator.Send(new UploadFileCommand
                {
                    Genre = "Product",
                    file = command.Image
                });
                if (!(oldImage == null || oldImage == "" || oldImage == "string"))
                {
                    // _fileServices.DeleteFile("Product", oldImage);
                    await _mediator.Send(new DeleteFileCommand
                    {
                        Genre = "Product",
                        fileNameWithExtension = oldImage
                    });
                }
            }
            item.Name = command.Name;
            item.Description = command.Description;
            item.Price = command.Price;
            item.Category = command.Category;
            item.Stock = command.Stock;
            item.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return item;
        }
    }
}