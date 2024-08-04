using CQRS.Ecommerce.Domain.Entities;
using MediatR;

namespace CQRS.Ecommerce.Application;

public class DeleteProductCommand : IRequest<ServiceResult<bool>>
{
    public Guid Id { get; set; }
}
