using Para.Data.Domain;
using Para.Data.GenericRepository;

namespace Para.Data.UnitOfWork;

public interface IUnitOfWork<T> where T : class
{
    Task Complete(); 
    Task CompleteWithTransaction();
    IGenericRepository<T> GenericRepository { get; }
}