using App.Services.Categories;
using App.Services.ExceptionHandler;
using App.Services.Filters;
using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Services.Extensions;
public static class ServiceExtension
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		//.net'in default validation filterini kapattik, cunku biz fluent validation kullanacagiz.
		services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

		services.AddScoped<IProductService, ProductService>();
		services.AddScoped<ICategoryService, CategoryService>();

		services.AddScoped(typeof(NotFoundFilter<,>));//ctorda parametre alan bagimli bir class oldugu icin IOC kaydi yapilir

		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		services.AddAutoMapper(Assembly.GetExecutingAssembly());

		services.AddExceptionHandler<CriticalExceptionHandler>();
		services.AddExceptionHandler<GlobalExceptionHandler>();

		return services;
	}
}
