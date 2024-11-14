using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using App_API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App_API.Controllers;
public class ProductsController(IProductService productService) : CustomBaseController
{
	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		var serviceResult = await productService.GetAllAsync();
		return CreateActionResult(serviceResult);
	}

	[HttpGet("{pageNumber:int}/{pageSize:int}")]
	public async Task<IActionResult> GetAllPagedAsync(int pageNumber, int pageSize)
	{
		var serviceResult = await productService.GetAllPagedAsync(pageNumber, pageSize);
		return CreateActionResult(serviceResult);
	}

	[ServiceFilter(typeof(NotFoundFilter<Product, int>))]
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var serviceResult = await productService.GetByIdAsync(id);
		return CreateActionResult(serviceResult);
	}

	[HttpPost]
	public async Task<IActionResult> CreateAsync(CreateProductRequest request)
	{
		var serviceResult = await productService.CreateAsync(request);
		return CreateActionResult(serviceResult);
	}

	[ServiceFilter(typeof(NotFoundFilter<Product, int>))]
	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateAsync(int id, UpdateProductRequest request)
	{
		var serviceResult = await productService.UpdateAsync(id, request);
		return CreateActionResult(serviceResult);
	}

	[HttpPatch("stock")]
	public async Task<IActionResult> UpdateStockAsync(UpdateProductStockRequest request)
	{
		var serviceResult = await productService.UpdateStockAsync(request);
		return CreateActionResult(serviceResult);
	}

	[ServiceFilter(typeof(NotFoundFilter<Product,int>))]
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteAsync(int id)
	{
		var serviceResult = await productService.DeleteAsync(id);
		return CreateActionResult(serviceResult);
	}
}
