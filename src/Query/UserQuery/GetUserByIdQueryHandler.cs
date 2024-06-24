using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Query.UserQuery
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly CQRSDbContext _dbContext;
        public GetUserByIdQueryHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Users
                .Include(c => c.userCredentials)
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (item == null)
            {
                throw new Exception(message: "User does not exist");
            }
            return item;
        }
    }
}
