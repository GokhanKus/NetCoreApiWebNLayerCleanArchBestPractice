using App.Repositories.Categories;
using App.Repositories.Interceptors;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions;
public static class RepositoryExtension
{
	public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration config)
	{
		services.AddDbContext<AppDbContext>(x =>
		{
			var connectionStrings = config.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
			x.UseSqlServer(connectionStrings!.SqlServer, sqlServerOptionsAction =>
			{
				sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
			});
			x.AddInterceptors(new AuditDbContextInterceptor());
		});
		//ilerde reflection kullanarak sonu repository ile bitenlerin IoC kaydini otomatik yapacagiz yoksa 40 tane olsa 40 kere addscoped & singleton mu yazacagiz..
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}

