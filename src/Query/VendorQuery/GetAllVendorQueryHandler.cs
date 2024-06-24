using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Query.VendorQuery
{
    public class GetAllVendorQueryHandler : IRequestHandler<GetAllVendorQuery, List<Vendor>>
    {
        private readonly CQRSDbContext _dbContext;
        public GetAllVendorQueryHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Vendor>> Handle(GetAllVendorQuery query, CancellationToken cancellationToken)
        {
            return await _dbContext.Vendors
                // .Include(c=>c.Products)
                .ToListAsync();
        }
    }
}
