using System;
using System.Security;
using Domain.Interface;

namespace Infrastructure.Repository;

public class ServiceFactory : IServiceFactory
{
    private readonly ApplicationDbContext _db;
    public ServiceFactory(ApplicationDbContext db)
    {
        _db = db;
    }
    public IServiceRepository<T> GetInstance<T>() where T : class
    {
        return new ServiceRepository<T>(_db);
    }

    public void BeginTransaction()
    {
        _db.Database.BeginTransaction();
    }
    public void RollBack()
    {
        _db.Database.RollbackTransaction();
    }
    public void Commit()
    {
        _db.Database.CommitTransaction();
    }
}
