using CQRS.Ecommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Ecommerce.Application.Features.Products.Requests
{
    public class GetAllProductQuery : IRequest<ServiceResult<List<Product>>>
    {

    }
}