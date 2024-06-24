using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Command.CustomerCommand
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, Cart>
    {
        private readonly CQRSDbContext _dbContext;
        public RemoveFromCartCommandHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cart> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                        .FirstOrDefaultAsync(x => x.Id == request.ProductId);
            if (product == null)
            {
                throw new Exception("Cannot find the product with the specified id");
            }
            //Finding cart details with the help of customerId
            var cartInformation = await _dbContext.Carts.Include(c => c.Items)
                        .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId);
            if (cartInformation == null)
            {
                throw new Exception("Cant find the cart details, Please create one.");
            }
            var cartItem = cartInformation.Items
                        .FirstOrDefault(ci => ci.ProductId == request.ProductId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > request.Quantity)
                {
                    cartItem.Quantity -= request.Quantity;
                }
                else
                {
                    cartInformation.Items.Remove(cartItem);
                    _dbContext.CartItems.Remove(cartItem);
                }
            }
            await _dbContext.SaveChangesAsync();
            cartInformation.Customer = null;
            cartInformation.Items = null;

            return cartInformation;
        }
    }
}