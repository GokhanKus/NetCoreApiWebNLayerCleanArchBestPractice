using System.Linq.Expressions;

namespace App.Repositories;
public interface IGenericRepository<T> where T : class
{
	IQueryable<T> Where(Expression<Func<T, bool>> predicate);
	IQueryable<T> GetAll();
	ValueTask<T?> GetByIdAsync(int id);
	ValueTask AddAsync(T entity);
	void Update(T entity);
	void Delete(T entity);
}
