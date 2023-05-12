using UserAPI.Core;

namespace UserAPI.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly UserDbContext context;

    public UnitOfWork(UserDbContext context)
    {
        this.context = context;
    }
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}