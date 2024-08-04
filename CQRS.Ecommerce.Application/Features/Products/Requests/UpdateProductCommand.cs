using MediatR;

namespace CQRS.Ecommerce.Application;

public class UpdateProductCommand : IRequest<ServiceResult<bool>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string Category { get; set; }
    public int Stock { get; set; }
    public Guid VendorId { get; set; }
}
