using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;
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

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteAsync(int id)
	{
		var serviceResult = await productService.DeleteAsync(id);
		return CreateActionResult(serviceResult);
	}
}
