using CQRSApplication.Context;
using CQRSApplication.Helpers;
using CQRSApplication.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSApplication.Command.ProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly CQRSDbContext _dbContext;
        private readonly IMediator _mediator;
        public CreateProductCommandHandler(CQRSDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task<Product> Handle([FromForm] CreateProductCommand request, CancellationToken cancellationToken)
        {
            //Todo: If request.UserId is valid user
            var vendor = await _dbContext.Vendors
                        .FindAsync(request.VendorId);
            if (vendor == null)
            {
                throw new Exception("Vendor does not exist");
            }

            var user = await _dbContext.Users
                        .FindAsync(vendor.UserId);
            if (user == null)
            {
                throw new Exception("User does not exist in current context");
            }

            string imageUrl = string.Empty;
            if (request.Image != null)
            {
                // ImageUrl = await _fileServices.UploadFile("Product", request.Image);
                imageUrl = await _mediator.Send(new UploadFileCommand
                {
                    Genre = "Product",
                    file = request.Image
                });
            }
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                Price = request.Price,
                Stock = request.Stock,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                VendorId = request.VendorId,
            };
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }
    }
}
