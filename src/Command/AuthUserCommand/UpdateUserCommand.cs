using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.AuthUserCommand
{

    public class UpdateUserCommand : IRequest<User>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IFormFile? Image { get; set; }
        public virtual UpdateUserCredentials UpdateUserCredentials { get; set; }
        public virtual UpdateShippingAddress ShippingAddress { get; set; }
    }

    public class UpdateUserCredentials
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string Email { get; set; }

    }

    public class UpdateShippingAddress
    {
        public string District { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }


}
