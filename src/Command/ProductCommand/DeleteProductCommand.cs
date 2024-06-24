using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.ProductCommand
{
    public class DeleteProductCommand : IRequest<Product>
    {
        public Guid ProductId { get; set; }
        public Guid VendorId { get; set; }
    }
}