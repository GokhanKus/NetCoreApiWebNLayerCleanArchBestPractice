using App.Application;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace App_API.ExceptionHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		var errorDto = ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError);

		httpContext.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
		httpContext.Response.ContentType = "application/json";
		await httpContext.Response.WriteAsJsonAsync(errorDto, cancellationToken);

		return true; //hata burada son bulacak, error dto'yu donecegiz
	}
}
