using App.Application.Features.Categories;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using App_API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App_API.Controllers;
public class CategoriesController(ICategoryService categoryService) : CustomBaseController
{
	[HttpGet]
	public async Task<IActionResult> GetCategories()
	{
		var serviceResult = await categoryService.GetAllListAsync();
		return CreateActionResult(serviceResult);
	}

	[ServiceFilter(typeof(NotFoundFilter<Category, int>))]
	[HttpGet("{id}")]
	public async Task<IActionResult> GetCategory(int id)
	{
		var serviceResult = await categoryService.GetByIdAsync(id);
		return CreateActionResult(serviceResult);
	}

	[HttpGet("products")]
	public async Task<IActionResult> GetCategoryWithProducts()
	{
		var serviceResult = await categoryService.GetCategoryWithProductsAsync();
		return CreateActionResult(serviceResult);
	}

	[ServiceFilter(typeof(NotFoundFilter<Category, int>))]
	[HttpGet("{id}/products")]
	public async Task<IActionResult> GetCategoryWithProducts(int id)
	{
		var serviceResult = await categoryService.GetCategoryWithProductsAsync(id);
		return CreateActionResult(serviceResult);
	}

	[HttpPost]
	public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
	{
		var serviceResult = await categoryService.CreateAsync(request);
		return CreateActionResult(serviceResult);
	}

	[ServiceFilter(typeof(NotFoundFilter<Category, int>))]
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request)
	{
		var serviceResult = await categoryService.UpdateAsync(id, request);
		return CreateActionResult(serviceResult);
	}

	[ServiceFilter(typeof(NotFoundFilter<Category, int>))]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteCategory(int id)
	{
		var serviceResult = await categoryService.DeleteAsync(id);
		return CreateActionResult(serviceResult);
	}
}
