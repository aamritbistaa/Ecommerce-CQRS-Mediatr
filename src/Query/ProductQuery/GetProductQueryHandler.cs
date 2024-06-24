using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Query.ProductQuery
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly CQRSDbContext _dbContext;
        public GetProductQueryHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> Handle(GetProductQuery req, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Products
                        .FirstOrDefaultAsync(a => a.Id == req.Id);
            if (item == null)
            {
                throw new Exception(message: "Cannot find the item with specified Id");
            }
            return item;
        }
    }
}
