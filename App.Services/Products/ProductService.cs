﻿using App.Repositories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace App.Services.Products;
public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
	public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
	{
		//List<> tipinde bir sey alirken null kontrolü yapmasak da olur gelmezse bos liste gelecektir if ile kalabalik yapilmayabilir
		var products = await productRepository.GetAll().ToListAsync();
		var productsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
		return ServiceResult<List<ProductDto>>.Success(productsDto);
	}
	public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
	{
		var products = await productRepository.GetTopPriceProductAsync(count);

		if (products is null)
			ServiceResult<List<ProductDto>>.Fail("products not found", HttpStatusCode.NotFound);

		var productDto = products!.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
		return ServiceResult<List<ProductDto>>.Success(productDto);
	}
	public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
	{
		var product = await productRepository.GetByIdAsync(id);

		if (product is null)
			return ServiceResult<ProductDto?>.Fail("product not found", HttpStatusCode.NotFound);

		var productDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);
		return ServiceResult<ProductDto>.Success(productDto)!;
	}
	public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
	{
		var product = new Product
		{
			Name = request.Name,
			Price = request.Price,
			Stock = request.Stock
		};

		await productRepository.AddAsync(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id), HttpStatusCode.Created);
	}
	public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
	{
		//update ve delete islemlerinde geriye bir sey donulmez 204 yani no content donebiliriz
		#region Fast Fail & Guard Clauses
		/* 2 tane clean code prensibi vardır 
		1 - Fast Fail 
	    once hata olabilecek durumlar kontrol edilir, olumsuz kodlar ilk başa yazılır ornegin if (product is null) gibi.. 
		2 - Guard Clauses 
		bu da fast fail gibidir, ek olarak mümkün oldugunca if else bloklarından kaçınılmalıdır, cunku ic ice gecmis uzun if else blokları
		hem kodun okunulabilirligini azaltıp hem de cyclomatic complexity(kod karmasasi)'yi artirir bunu if elseler ve metodun kendisi 1 artirir
		hic if else yoksa cyclomatic complexity 1 dir bu ne kadar az olursa o kadar iyidir.
		*/

		#endregion

		var product = await productRepository.GetByIdAsync(id);

		if (product is null)
			return ServiceResult.Fail("product not found", HttpStatusCode.NotFound);

		product.Name = request.Name;
		product.Price = request.Price;
		product.Stock = request.Stock;

		productRepository.Update(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}
	public async Task<ServiceResult> DeleteAsync(int id)
	{
		var product = await productRepository.GetByIdAsync(id);

		if (product is null)
			return ServiceResult.Fail("product not found", HttpStatusCode.NotFound);

		productRepository.Delete(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}
}
