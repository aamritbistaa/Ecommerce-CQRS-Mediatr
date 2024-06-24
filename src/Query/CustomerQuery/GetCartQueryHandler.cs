using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Query.CustomerQuery
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, List<CartItem>>
    {
        private readonly CQRSDbContext _dbContext;
        public GetCartQueryHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<CartItem>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                        .FindAsync(request.CustomerId)
                        ?? throw new Exception("Cant find User with the id");
            var cartItem = await _dbContext.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId);
            if (cartItem == null)
            {
                throw new Exception(message: "Cart does not exist for the specified user id");
            }
            if (cartItem.Items == null || cartItem.Items.Count == 0)
            {
                throw new Exception(message: "Cart is empty");
            }
            List<CartItem> ItemCollection = new List<CartItem>();
            foreach (var item in cartItem.Items)
            {
                ItemCollection.Add(new CartItem
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    CartId = item.CartId,
                    Cart = null,
                });
            }
            return ItemCollection;

        }
    }
}