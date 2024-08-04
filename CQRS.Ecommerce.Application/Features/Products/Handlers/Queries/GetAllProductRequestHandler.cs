using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CQRS.Ecommerce.Application.Features.Products.Requests;
using CQRS.Ecommerce.Domain;
using CQRS.Ecommerce.Domain.Entities;
using MediatR;

namespace CQRS.Ecommerce.Application.Features.Products.Handlers.Queries
{
    public class GetAllProductRequestHandler : IRequestHandler<GetAllProductQuery, ServiceResult<List<Product>>>
    {
        public IProductService _service;

        public GetAllProductRequestHandler(IProductService service)
        {
            _service = service;
        }

        public async Task<ServiceResult<List<Product>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {

            var data = await _service.GetAllProductAsync();
            if (data.Any())
            {

                return new ServiceResult<List<Product>>
                {
                    StatusCode = StatusCode.Success,
                    Message = Message.Success,
                    Data = data,
                };
            }
            return new ServiceResult<List<Product>>
            {
                StatusCode = StatusCode.Success,
                Message = Message.Null,
                Data = new List<Product>()
            };
        }
    }
}