using CQRSApplication.Model;
using MediatR;

namespace CQRSApplication.Query.CustomerQuery
{
    public class GetCustomerByIdQuery : IRequest<User>
    {
        public Guid Id { get; set; }
    }
}