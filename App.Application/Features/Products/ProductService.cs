using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistence;
using App.Application.Contracts.ServiceBus;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using App.Domain.Event;
using AutoMapper;
using System.Net;

namespace App.Application.Features.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper,ICacheService cacheService,IServiceBus serviceBus) : IProductService
{
	private const string ProductListCacheKey = "ProductListCacheKey";
	public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
	{
		//cachelemeyi bu sekilde yapmak yerine decorator veya proxy desing pattern'ı ile yapmak daha iyi olur (Training web api projemde yapmıstım)
		//List<> tipinde bir sey alirken null kontrolü yapmasak da olur gelmezse bos liste gelecektir if ile kalabalik yapilmayabilir
		var productListAsCached = await cacheService.GetAsync<List<ProductDto>>(ProductListCacheKey);
		if (productListAsCached is not null) return ServiceResult<List<ProductDto>>.Success(productListAsCached);

		var products = await productRepository.GetAllAsync();


		//var productsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList(); manuel mapping
		var productsDto = mapper.Map<List<ProductDto>>(products);

		await cacheService.AddAsync(ProductListCacheKey, productsDto, TimeSpan.FromDays(1));
		return ServiceResult<List<ProductDto>>.Success(productsDto);
	}
	public async Task<ServiceResult<List<ProductDto>>> GetAllPagedAsync(int pageNumber, int pageSize)
	{
		var products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);

		//var productsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList(); manuel mapping
		var productsDto = mapper.Map<List<ProductDto>>(products);

		return ServiceResult<List<ProductDto>>.Success(productsDto);
	}
	public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
	{
		var products = await productRepository.GetTopPriceProductAsync(count);

		if (products is null)
			ServiceResult<List<ProductDto>>.Fail("products not found", HttpStatusCode.NotFound);

		//var productDto = products!.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList(); manuel mapping
		var productDto = mapper.Map<List<ProductDto>>(products);
		return ServiceResult<List<ProductDto>>.Success(productDto);
	}
	public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
	{
		var product = await productRepository.GetByIdAsync(id);

		//var productDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock); manuel mapping
		var productDto = mapper.Map<ProductDto>(product);
		return ServiceResult<ProductDto>.Success(productDto)!;
	}
	public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
	{
		//throw new CriticalException("kritik bir hata meydana geldi");
		//async manuel service business check
		var anyProduct = await productRepository.AnyAsync(p => p.Name == request.Name);
		if (anyProduct)
			return ServiceResult<CreateProductResponse>.Fail("product name is already exists in database");

		var product = mapper.Map<Product>(request);

		await productRepository.AddAsync(product);
		await unitOfWork.SaveChangesAsync();
		await serviceBus.PublishAsync(new ProductAddedEvent(product.Id, product.Name, product.Price));
		return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
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

		var isProductNameExist = await productRepository.AnyAsync(p => p.Name == request.Name && p.Id != id);
		if (isProductNameExist)
			return ServiceResult.Fail("product name is already exists in database");

		var product = mapper.Map<Product>(request);
		product.Id = id;

		productRepository.Update(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}
	public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
	{
		var product = await productRepository.GetByIdAsync(request.ProductId);

		if (product is null)
			return ServiceResult.Fail("product not found", HttpStatusCode.NotFound);

		product.Stock = request.Quantity;

		productRepository.Update(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}
	public async Task<ServiceResult> DeleteAsync(int id)
	{
		var product = await productRepository.GetByIdAsync(id);

		productRepository.Delete(product!);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}

}
