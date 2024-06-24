using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Query.ProductQuery
{
    public class GetProductQuery:IRequest<Product>
    {
        public Guid Id { get; set; }
    }
}
