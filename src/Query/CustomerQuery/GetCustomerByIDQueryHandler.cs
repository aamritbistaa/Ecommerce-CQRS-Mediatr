using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Query.CustomerQuery
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, User>
    {
        private readonly CQRSDbContext _dbContext;
        public GetCustomerByIdQueryHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(u => u.shippingAddress)
                // .Include(u => u.userCredentials)
                //  .Include(u => u.Cart)
                .FirstOrDefaultAsync(u => u.Id == request.Id && u.userCredentials.Role == RoleType.Customer);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }
    }
}