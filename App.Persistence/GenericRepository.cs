using App.Application.Contracts.Persistence;
using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Persistence;
public class GenericRepository<T, Tid>(AppDbContext context) : IGenericRepository<T, Tid> where T : BaseEntity<Tid> where Tid : struct
{
	protected readonly AppDbContext _context = context;
	private readonly DbSet<T> _dbSet = context.Set<T>();

	public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();
	public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _dbSet.AnyAsync(predicate);
	public Task<bool> AnyAsync(Tid id) => _dbSet.AnyAsync(x => x.Id.Equals(id));
	public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();
	public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize) => _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
	public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
	public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
	public void Update(T entity) => _dbSet.Update(entity);
	public void Delete(T entity) => _dbSet.Remove(entity);
}
