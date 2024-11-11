﻿using App.Services.ExceptionHandler;
using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Services.Extensions;
public static class ServiceExtension
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<IProductService, ProductService>();

		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		services.AddAutoMapper(Assembly.GetExecutingAssembly());

		services.AddExceptionHandler<CriticalExceptionHandler>();
		services.AddExceptionHandler<GlobalExceptionHandler>();

		return services;
	}
}
