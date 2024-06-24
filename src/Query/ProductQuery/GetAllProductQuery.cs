using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Query.ProductQuery
{
    public class GetAllProductQuery : IRequest<List<Product>>
    {
    }
}
