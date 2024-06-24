using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Query.CustomerQuery
{
    public class GetAllCustomerQuery : IRequest<List<User>>
    {
    }
}