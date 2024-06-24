using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Query.UserQuery
{
    public class GetAllUserQuery:IRequest<List<User>>
    {
    }
}
