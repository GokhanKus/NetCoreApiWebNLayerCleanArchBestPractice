using App.Application;
using App.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App_API.Filters;
public class NotFoundFilter<T, Tid>(IGenericRepository<T, Tid> genericRepository) : Attribute, IAsyncActionFilter where T : class where Tid : struct
{
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		var idValue = context.ActionArguments.TryGetValue("id", out var idAsObject) ? idAsObject : null;

		if (idValue is not Tid id)
		{
			await next();
			return;
		}

		var isAnyEntity = await genericRepository.AnyAsync(id);

		if (isAnyEntity)
		{
			await next();
			return;
		}
		var entityName = typeof(T).Name;
		var actionName = context.ActionDescriptor.RouteValues["action"];

		var result = ServiceResult.Fail($"data not found ({entityName}) ({actionName})");
		context.Result = new NotFoundObjectResult(result);
	}
}
