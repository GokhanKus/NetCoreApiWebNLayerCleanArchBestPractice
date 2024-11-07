using App.Repositories.Products;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace App.Services.Products;
public class ProductService(IProductRepository productRepository) : IProductService
{
	public async Task<ServiceResult<List<Product>>> GetTopPriceProductsAsync(int count)
	{
		var products = await productRepository.GetTopPriceProductAsync(count);

		if (products is null)
			ServiceResult<List<Product>>.Fail("products not found", HttpStatusCode.NotFound);

		return ServiceResult<List<Product>>.Success(products!);
	}
	public async Task<ServiceResult<Product>> GetProductByIdAsync(int id)
	{
		var product = await productRepository.GetByIdAsync(id);

		if (product is null)
			ServiceResult<Product>.Fail("product not found", HttpStatusCode.NotFound);

		return ServiceResult<Product>.Success(product!);
	}
}
