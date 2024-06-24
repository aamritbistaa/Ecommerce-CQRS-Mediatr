using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.CustomerCommand
{
    public class AddToCartCommand : IRequest<Cart>
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}