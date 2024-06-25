namespace BodecashAPI.Shared.Persistence.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}