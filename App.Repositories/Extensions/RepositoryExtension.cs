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
		});
		return services;
	}
}

