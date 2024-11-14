using App.Application.Contracts.Persistence;
using App.Persistence;

namespace App.Persistence;
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
	public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
