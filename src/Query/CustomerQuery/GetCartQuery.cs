using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Query.CustomerQuery
{
    public class GetCartQuery : IRequest<List<CartItem>>
    {
        public Guid CustomerId { get; set; }
    }
}