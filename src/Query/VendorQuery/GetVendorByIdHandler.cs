using CQRSApplication.Context;
using CQRSApplication.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Query.VendorQuery
{
    public class GetVendorByIdHandler : IRequestHandler<GetVendorById, Vendor>
    {
        private readonly CQRSDbContext _dbContext;
        public GetVendorByIdHandler(CQRSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Vendor> Handle(GetVendorById query, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Vendors
                        .Include(c => c.Products)
                        .FirstOrDefaultAsync(v => v.Id == query.VendorId);
            if (item == null)
            {
                throw new Exception($"Vendor with Id {query.VendorId} not found");
            }
            return item;
        }
    }
}
