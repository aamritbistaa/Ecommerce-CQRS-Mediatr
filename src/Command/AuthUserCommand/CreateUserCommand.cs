using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.AuthUserCommand
{
    public class CreateUserCommand : IRequest<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IFormFile? Image { get; set; }
        public virtual CreateUserCredentials UserCredentials { get; set; }
        public virtual ShippingDetails ShippingDetails { get; set; }
    }
    public class CreateUserCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    public class ShippingDetails
    {
        public string District { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
    }
}
