using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Query.ProductQuery
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<Product>>
    {
        public readonly CQRSDbContext _dbContext;
        public GetAllProductQueryHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Product>> Handle(GetAllProductQuery req, CancellationToken cancellationToken)
        {
            //return await _context.Products.Include(c => c.User).ToListAsync();
            return await _dbContext.Products.ToListAsync();
        }
    }
}
