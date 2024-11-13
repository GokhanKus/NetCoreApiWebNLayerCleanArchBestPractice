using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Repositories;
public class GenericRepository<T, Tid>(AppDbContext context) : IGenericRepository<T, Tid> where T : BaseEntity<Tid> where Tid : struct
{
	protected readonly AppDbContext _context = context;
	private readonly DbSet<T> _dbSet = context.Set<T>();
	public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();
	public Task<bool> AnyAsync(Tid id) => _dbSet.AnyAsync(x => x.Id.Equals(id));
	public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();
	public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
	public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
	public void Update(T entity) => _dbSet.Update(entity);
	public void Delete(T entity) => _dbSet.Remove(entity);

}
