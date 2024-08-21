using System;

namespace Domain.Interface;

public interface IServiceFactory
{
    IServiceRepository<T> GetInstance<T>() where T : class;
    void BeginTransaction();
    void RollBack();
    void Commit();
}
