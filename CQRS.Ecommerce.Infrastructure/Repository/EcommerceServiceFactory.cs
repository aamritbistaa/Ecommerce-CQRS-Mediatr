using CQRS.Ecommerce.Domain;
using CQRS.Ecommerce.Infrastructure.Repository;

namespace CQRS.Ecommerce.Infrastructure;

public class EcommerceServiceFactory : IEcommerceServiceFactory
{
    private readonly EcommerceDbContext db;

    public EcommerceServiceFactory(EcommerceDbContext db)
    {
        this.db = db;
    }
    public IEcommerceServiceRepository<T> GetInstance<T>() where T : class
    {
        return new EcommerceServiceRepository<T>(db);
    }

}
