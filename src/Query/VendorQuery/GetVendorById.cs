using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Query.VendorQuery
{
    public class GetVendorById : IRequest<Vendor>
    {
        public Guid VendorId { get; set; }
    }
}
