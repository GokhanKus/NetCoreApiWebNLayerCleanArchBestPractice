using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repositories;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) //primary ctor with .net 8.0
{
	public DbSet<Product> Products { get; set; } = default!;
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}
}

