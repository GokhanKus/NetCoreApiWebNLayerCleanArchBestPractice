using App.Application.Features.Categories;
using App.Application.Features.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Application.Extensions;
public static class ServiceExtension
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		//.net'in default validation filterini kapattik, cunku biz fluent validation kullanacagiz.
		services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

		services.AddScoped<IProductService, ProductService>();
		services.AddScoped<ICategoryService, CategoryService>();

		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		services.AddAutoMapper(Assembly.GetExecutingAssembly());


		return services;
	}
}
