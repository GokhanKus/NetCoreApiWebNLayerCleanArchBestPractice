using App_API.Filters;

namespace App_API.Extension;
public static class ControllerExtensions
{
	public static IServiceCollection AddControllersWithFiltersExt(this IServiceCollection services)
	{
		services.AddScoped(typeof(NotFoundFilter<,>));//ctorda parametre alan bagimli bir class oldugu icin IOC kaydi yapilir

		services.AddControllers(options =>
		{
			options.Filters.Add<FluentValidationFilter>();
			options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
		});
		return services;
	}
}
