using MediatR;

namespace CQRS.Ecommerce.Application;

public class PermaDeleteProductCommand : IRequest<ServiceResult<bool>>
{
    public Guid Id { get; set; }
}
