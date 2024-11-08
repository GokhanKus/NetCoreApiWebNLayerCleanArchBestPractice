using App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
	//bunlar birer endpoint degil, bunlar yardımcı metotlar, o yuzden swagger endpoint gibi algılamasın
	[NonAction]
	public IActionResult CreateActionResult<T>(ServiceResult<T> result)
	{
		return result.Status switch
		{
			HttpStatusCode.NoContent => NoContent(),
			HttpStatusCode.Created => Created(result.UrlAsCreated, result.Data),
			_ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
		};
	}

	[NonAction]
	public IActionResult CreateActionResult(ServiceResult result)
	{
		if (result.Status == HttpStatusCode.NoContent)
		{
			return new ObjectResult(null) { StatusCode = result.Status.GetHashCode() };
		}
		return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
	}
}
