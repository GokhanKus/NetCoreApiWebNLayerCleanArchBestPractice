using System.Linq.Expressions;

namespace App.Repositories;
public interface IGenericRepository<T, Tid> where T : class where Tid : struct
{
	IQueryable<T> Where(Expression<Func<T, bool>> predicate);
	Task<bool> AnyAsync(Tid id);
	IQueryable<T> GetAll();
	ValueTask<T?> GetByIdAsync(int id);
	ValueTask AddAsync(T entity);
	void Update(T entity);
	void Delete(T entity);
}
