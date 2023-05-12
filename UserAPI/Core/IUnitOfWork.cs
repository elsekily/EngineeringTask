namespace UserAPI.Core;

public interface IUnitOfWork
{
    Task CompleteAsync();
}