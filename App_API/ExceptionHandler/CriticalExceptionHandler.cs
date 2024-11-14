using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace App_API.ExceptionHandler;
public class CriticalExceptionHandler : IExceptionHandler
{
	public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		if(exception is CriticalException)
            Console.WriteLine("hata ile ilgili mesaj gonderildi");

		return ValueTask.FromResult(false);
		//geriye false donerek, hatayi varsa bir sonraki handler'a(GlobalExceptionHandler) gonderiririz, bir sonraki handler yoksa middleware'ye gider
	}
}
