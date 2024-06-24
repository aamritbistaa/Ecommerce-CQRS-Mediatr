using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Query.UserQuery
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<User>>
    {
        private readonly CQRSDbContext _dbContext;
        public GetAllUserQueryHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Users
                        .Include(c => c.userCredentials)
                        .ToListAsync();
            if (item == null)
            {
                throw new Exception(message: "User does not exist");
            }
            return item;
        }
    }
}
