using CQRS.Ecommerce.Domain.Entities;
using MediatR;

namespace CQRS.Ecommerce.Application;

public class GetProductByIdQuery : IRequest<ServiceResult<Product>>
{
    public Guid Id { get; set; }
}
