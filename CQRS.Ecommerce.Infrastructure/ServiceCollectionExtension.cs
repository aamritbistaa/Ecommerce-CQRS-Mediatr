using CQRS.Ecommerce.Domain;
using CQRS.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Ecommerce.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastrcucture(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EcommerceDbContext>(options => options.UseNpgsql(connectionString));

            // services.AddScoped<IEcommerceSeeder, EcommerceSeeder>();
            services.AddScoped<IProductService, ProductServices>();

        }
    }
}
