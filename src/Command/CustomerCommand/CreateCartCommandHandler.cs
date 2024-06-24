using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Command.CustomerCommand
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Cart>
    {
        private readonly CQRSDbContext _dbContext;
        public CreateCartCommandHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cart> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var customerDetail = await _dbContext.Users
                        .FirstOrDefaultAsync(c => c.Id == request.CustomerId);
            if (customerDetail == null) throw new Exception("Cant find the customer.");

            var existingCart = await _dbContext.Carts
                        .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId);
            if (existingCart != null)
            {
                return existingCart;
            }
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                Customer = null
            };
            customerDetail.CartId = cart.Id;

            await _dbContext.Carts.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }
    }
}