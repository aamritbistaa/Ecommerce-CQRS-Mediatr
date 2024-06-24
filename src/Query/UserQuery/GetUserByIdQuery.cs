using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Query.UserQuery
{
    public class GetUserByIdQuery:IRequest<User>
    {
        public Guid Id { get; set; }
    }
}
