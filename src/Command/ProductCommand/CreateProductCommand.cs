using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.ProductCommand
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? Category { get; set; }
        public int Stock { get; set; }
        public IFormFile? Image { get; set; }
        public Guid VendorId { get; set; }
    }
}
