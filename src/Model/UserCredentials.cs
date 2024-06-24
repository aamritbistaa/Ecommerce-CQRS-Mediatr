using System.ComponentModel.DataAnnotations;

namespace CQRSApplication.Model
{
    public class UserCredentials
    {
        [Key]
        public Guid Id { get; set; }

        [Required, EmailAddress, MaxLength(20)]
        public string Email { get; set; }

        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required, MaxLength(20)]
        public string Password { get; set; }
        public RoleType Role { get; set; } = RoleType.Customer;
        public string? Token { get; set; }
        public bool IsActive { get; set; }
    }
    public enum RoleType
    {
        SuperAdmin,
        Vendor,
        Customer
    }
}
