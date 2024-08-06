namespace CQRS.Ecommerce.Domain;

public interface IEcommerceServiceFactory
{
    IEcommerceServiceRepository<T> GetInstance<T>() where T : class;
}
