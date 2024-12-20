﻿using System.Net;
using System.Text.Json.Serialization;

namespace App.Application;
public class ServiceResult<T>
{
	public T? Data { get; set; }
	public List<string>? ErrorMessages { get; set; }
	[JsonIgnore] public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;
	[JsonIgnore] public bool IsFail => !IsSuccess;
	[JsonIgnore] public HttpStatusCode Status { get; set; }
	[JsonIgnore] public string? UrlAsCreated { get; set; }

	//static factory methodlar newlemeyi kontrol altina alir, surekli newlemenin onune gecer(orn Product Service icin bak)
	public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
	{
		return new ServiceResult<T>
		{
			Data = data,
			Status = status
		};
	}
	public static ServiceResult<T> SuccessAsCreated(T data,string urlAsCreated)
	{
		return new ServiceResult<T>
		{
			Data = data,
			Status = HttpStatusCode.Created,
			UrlAsCreated = urlAsCreated
		};
	}
	public static ServiceResult<T> Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
	{
		return new ServiceResult<T>
		{
			ErrorMessages = errorMessages,
			Status = status
		};
	}
	public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
	{
		return new ServiceResult<T>
		{
			ErrorMessages = [errorMessage],
			Status = status
		};
	}
}
public class ServiceResult
{
	public List<string>? ErrorMessages { get; set; }
	//swagger veya postman tarafinda bu proplarin gorunmesine gerek yok
	[JsonIgnore] public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;
	[JsonIgnore] public bool IsFail => !IsSuccess;
	[JsonIgnore] public HttpStatusCode Status { get; set; }

	public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
	{
		return new ServiceResult
		{
			Status = status
		};
	}
	public static ServiceResult Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
	{
		return new ServiceResult
		{
			ErrorMessages = errorMessages,
			Status = status
		};
	}
	public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
	{
		return new ServiceResult
		{
			ErrorMessages = [errorMessage],
			Status = status
		};
	}
}