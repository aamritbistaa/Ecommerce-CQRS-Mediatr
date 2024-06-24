using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSApplication.Model
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual User Customer { get; set; }
    }

    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        // Foreign key to Cart
        [ForeignKey("CartId")]
        public Guid CartId { get; set; }
        // Navigation property for Cart
        public virtual Cart? Cart { get; set; }
    }
}