using System.ComponentModel.DataAnnotations;
using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.ProductCommand
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? Category { get; set; }
        public int Stock { get; set; }
        public IFormFile? Image { get; set; }
        public Guid VendorId { get; set; }
    }
}