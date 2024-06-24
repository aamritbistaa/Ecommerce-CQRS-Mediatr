using System.Data;
using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Query.CustomerQuery
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, List<User>>
    {
        private readonly CQRSDbContext _dbContext;
        public GetAllCustomerQueryHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<User>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            //var item = await _dbContext.Users.Where(c => c.userCredentials.Role == RoleType.Customer).ToListAsync();
            var customer = await _dbContext.Users
                        // .Include(u => u.userCredentials)
                        .Where(u => u.userCredentials.Role == RoleType.Customer)
                        .ToListAsync(cancellationToken);
            return customer;
        }
    }
}