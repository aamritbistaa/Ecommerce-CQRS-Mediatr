using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.CustomerCommand
{
    public class CreateCartCommand : IRequest<Cart>
    {
        public Guid CustomerId { get; set; }
    }
}