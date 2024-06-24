using CQRSApplication.Command.ProductCommand;
using CQRSApplication.Context;
using CQRSApplication.Helpers;
using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.ProductCommandHandler
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Product>
    {
        private readonly CQRSDbContext _dbContext;
        private readonly IMediator _mediator;
        public DeleteProductCommandHandler(CQRSDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task<Product> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            //Todo: check if item.userId is same with USERID()
            var item = await _dbContext.Products
                        .FindAsync(request.ProductId)
                        ?? throw new Exception(message: "Item does not exit");

            if (request.VendorId != item.VendorId)
            {
                throw new Exception(message: "You dont have privilage to delete this item");
            }
            if (!(item.ImageUrl == null || item.ImageUrl == "" || item.ImageUrl == "string"))
            // if (!string.IsNullOrWhiteSpace(item.ImageUrl))
            {
                // _fileServices.DeleteFile("Product", item.ImageUrl);
                await _mediator.Send(new DeleteFileCommand
                {
                    Genre = "Product",
                    fileNameWithExtension = item.ImageUrl
                });
            }
            _dbContext.Products.Remove(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}