using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Command.AuthUserCommand
{
    public class DeleteUserCommand : IRequest<User>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
