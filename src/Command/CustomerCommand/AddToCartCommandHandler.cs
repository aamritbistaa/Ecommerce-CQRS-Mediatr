using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Command.CustomerCommand
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Cart>
    {
        private readonly CQRSDbContext _dbContext;
        public AddToCartCommandHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cart> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                    .FirstOrDefaultAsync(x => x.Id == request.ProductId);
            if (product == null)
            {
                throw new Exception("Cannot find the product with the specified id");
            }

            //Finding cart details with the help of customerId
            var cartInformation = await _dbContext.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId);
            if (cartInformation == null)
            {
                throw new Exception("Cant find the cart details, Please create one.");
            }

            var cartItem = cartInformation.Items.FirstOrDefault(ci => ci.ProductId == request.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += request.Quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    CartId = cartInformation.Id
                };
                cartInformation.Items.Add(cartItem);
                _dbContext.CartItems.Add(cartItem);
            }
            await _dbContext.SaveChangesAsync();
            cartInformation.Customer = null;
            cartInformation.Items = null;
            return cartInformation;
        }
    }
}