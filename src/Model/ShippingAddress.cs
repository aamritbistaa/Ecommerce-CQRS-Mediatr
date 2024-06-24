using System.ComponentModel.DataAnnotations;

namespace CQRSApplication.Model
{
    public class ShippingAddress
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(20)]
        public string District { get; set; }
        [Required, MaxLength(20)]
        public string City { get; set; }
        [Required, MaxLength(20)]
        public string StreetAddress { get; set; }
    }
}