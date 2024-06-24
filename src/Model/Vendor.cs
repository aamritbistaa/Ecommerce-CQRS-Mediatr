using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSApplication.Model
{
    public class Vendor
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string ShopName { get; set; } = "Default Shop Name";
        [Required, MaxLength(200)]
        public string ShopAddress { get; set; } = "Default Shop Address";
        [Required, MaxLength(20)]
        public string PanNo { get; set; } = "Default Shop Pan";
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
