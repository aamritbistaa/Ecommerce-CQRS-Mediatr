using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Ecommerce.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required, StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }
        [StringLength(80, MinimumLength = 4)]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        public Guid VendorId { get; set; }
    }
}
