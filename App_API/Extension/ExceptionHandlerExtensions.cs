using App_API.ExceptionHandler;

namespace App_API.Extension;
public static class ExceptionHandlerExtensions
{
	public static IServiceCollection AddExceptionHandlerExt(this IServiceCollection services)
	{
		services.AddExceptionHandler<CriticalExceptionHandler>();
		services.AddExceptionHandler<GlobalExceptionHandler>();

		return services;
	}
}
