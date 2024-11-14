using System.Linq.Expressions;

namespace App.Application.Contracts.Persistence;
public interface IGenericRepository<T, Tid> where T : class where Tid : struct
{
	IQueryable<T> Where(Expression<Func<T, bool>> predicate);
	Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
	Task<bool> AnyAsync(Tid id);
	Task<List<T>> GetAllAsync();
	Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize);
	ValueTask<T?> GetByIdAsync(int id);
	ValueTask AddAsync(T entity);
	void Update(T entity);
	void Delete(T entity);
}
