using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Query.VendorQuery
{
    public class GetAllVendorQuery:IRequest<List<Vendor>>
    {
    }
}
